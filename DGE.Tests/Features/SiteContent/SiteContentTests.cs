using AutoMapper;
using DGE.Core.Features.Banners.Queries.Handler;
using DGE.Core.Features.Banners.Queries.RequestModels;
using DGE.Core.Features.CompanySettings.Commands.Handler;
using DGE.Core.Features.CompanySettings.Commands.RequestModels;
using DGE.Core.Features.CompanySettings.Queries.Handler;
using DGE.Core.Features.CompanySettings.Queries.RequestModels;
using DGE.Core.Features.Dashboard.Queries.Handler;
using DGE.Core.Features.Dashboard.Queries.RequestModels;
using DGE.Core.Features.MetaTags.Commands.Handler;
using DGE.Core.Features.MetaTags.Commands.RequestModels;
using DGE.Core.Features.MetaTags.Queries.Handler;
using DGE.Core.Features.MetaTags.Queries.RequestModels;
using DGE.Core.Features.SiteSections.Commands.Handler;
using DGE.Core.Features.SiteSections.Commands.RequestModels;
using DGE.Core.Features.SiteSections.Commands.Validators;
using DGE.Core.Features.SiteSections.Queries.Handler;
using DGE.Core.Features.SiteSections.Queries.RequestModels;
using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Core.Features.SocialMedias.Commands.Validators;
using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.Repository;
using DGE.Service.Service;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using DGE.Tests.TestDoubles;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;

namespace DGE.Tests.Features.SiteContent
{
    public class SiteContentTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly Mock<IAuthenticatedUserService> _authenticatedUserService = new();
        private readonly Mock<IFileService> _fileService = new();
        private readonly IMapper _mapper;
        private readonly KeyEchoLocalizer _localizer = new();

        public SiteContentTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            using (var context = CreateContext())
                context.Database.EnsureCreated();

            _authenticatedUserService.Setup(s => s.GetAuthenticatedUserName()).Returns("admin");
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DGE.Core.Mapping.SiteSectionMapping.SiteSectionMapping>();
                cfg.AddProfile<DGE.Core.Mapping.MetaTagMapping.MetaTagMapping>();
                cfg.AddProfile<DGE.Core.Mapping.CompanySettingsMapping.CompanySettingsMapping>();
                cfg.AddProfile<DGE.Core.Mapping.BannerMapping.BannerMapping>();
            }).CreateMapper(type => Activator.CreateInstance(type, _authenticatedUserService.Object)!);
        }

        public void Dispose() => _connection.Dispose();

        private AppDbContext CreateContext() =>
            new(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options);

        private SiteSectionService SiteSectionService(AppDbContext context) =>
            new(_localizer, new SiteSectionRepository(context, _localizer), NullLogger<BaseService<SiteSection>>.Instance);

        private MetaTagService MetaTagService(AppDbContext context) =>
            new(_localizer, new MetaTagRepository(context, _localizer), NullLogger<BaseService<MetaTag>>.Instance);

        private CompanySettingsService CompanySettingsService(AppDbContext context) =>
            new(_localizer, new CompanySettingsRepository(context, _localizer), NullLogger<BaseService<CompanySettings>>.Instance);

        private static List<SiteSectionItemRequestModel> Items(params string[] titles) =>
            titles.Select(t => new SiteSectionItemRequestModel { Title = t, Icon = "icon-" + t }).ToList();

        [Fact]
        public async Task SiteSection_GetByKey_ReturnsEmptyForUnsavedKey_AndNotFoundForUnknownKey()
        {
            using var context = CreateContext();
            var query = new SiteSectionHandlerQuery(SiteSectionService(context), _localizer, _mapper);

            var empty = await query.Handle(new GetSiteSectionByKeyQueryRequestModel { Key = "About" }, CancellationToken.None);
            var unknown = await query.Handle(new GetSiteSectionByKeyQueryRequestModel { Key = "careers" }, CancellationToken.None);

            Assert.Equal(HttpStatusCode.OK, empty.StatusCode);
            Assert.Equal("about", empty.Data!.Key);
            Assert.Equal(0, empty.Data.Id);
            Assert.Empty(empty.Data.Items);
            Assert.Equal(HttpStatusCode.NotFound, unknown.StatusCode);
        }

        [Fact]
        public async Task SiteSection_Update_CreatesThenUpdates_AndReplacesItemsOnlyWhenSent()
        {
            using (var context = CreateContext())
            {
                var handler = new SiteSectionCommandHandler(_localizer, _mapper, SiteSectionService(context), _authenticatedUserService.Object, _fileService.Object);
                Assert.True((await handler.Handle(new UpdateSiteSectionCommandRequestModel { Key = "quality", Title = "Quality", Details = "<p>v1</p>", Items = Items("a", "b", "c") }, CancellationToken.None)).Succeeded);
            }
            using (var context = CreateContext())
            {
                var handler = new SiteSectionCommandHandler(_localizer, _mapper, SiteSectionService(context), _authenticatedUserService.Object, _fileService.Object);
                // No items sent: the existing ones stay
                Assert.True((await handler.Handle(new UpdateSiteSectionCommandRequestModel { Key = "quality", Title = "Quality 2", Details = "<p>v2</p>" }, CancellationToken.None)).Succeeded);
            }
            using (var context = CreateContext())
            {
                Assert.Equal(1, await context.Set<SiteSection>().CountAsync());
                Assert.Equal(3, await context.Set<SiteSectionItem>().CountAsync());

                var handler = new SiteSectionCommandHandler(_localizer, _mapper, SiteSectionService(context), _authenticatedUserService.Object, _fileService.Object);
                Assert.True((await handler.Handle(new UpdateSiteSectionCommandRequestModel { Key = "quality", Title = "Quality 3", Items = Items("y", "x") }, CancellationToken.None)).Succeeded);
            }
            using (var context = CreateContext())
            {
                // Replaced items are deleted, not left orphaned
                Assert.Equal(2, await context.Set<SiteSectionItem>().CountAsync());

                var dto = (await new SiteSectionHandlerQuery(SiteSectionService(context), _localizer, _mapper)
                    .Handle(new GetSiteSectionByKeyQueryRequestModel { Key = "quality" }, CancellationToken.None)).Data!;
                Assert.Equal("Quality 3", dto.Title);
                Assert.Equal(["y", "x"], dto.Items.Select(i => i.Title));
                Assert.Equal("icon-y", dto.Items[0].Icon);

                var handler = new SiteSectionCommandHandler(_localizer, _mapper, SiteSectionService(context), _authenticatedUserService.Object, _fileService.Object);
                Assert.True((await handler.Handle(new UpdateSiteSectionCommandRequestModel { Key = "quality", Title = "Quality 3", ClearItems = true }, CancellationToken.None)).Succeeded);
            }
            using (var context = CreateContext())
                Assert.Equal(0, await context.Set<SiteSectionItem>().CountAsync());
        }

        [Fact]
        public async Task SiteSection_Validator_RejectsUnknownKeys()
        {
            var validator = new UpdateSiteSectionCommandValidator(_localizer);
            Assert.True((await validator.ValidateAsync(new UpdateSiteSectionCommandRequestModel { Key = "Export" })).IsValid);
            Assert.False((await validator.ValidateAsync(new UpdateSiteSectionCommandRequestModel { Key = "careers" })).IsValid);
            Assert.False((await validator.ValidateAsync(new UpdateSiteSectionCommandRequestModel())).IsValid);
        }

        [Fact]
        public async Task MetaTag_Update_UpsertsByPage_AndKeepsTheImageWhenNoneIsSent()
        {
            _fileService.Setup(s => s.UploadFileAsync("MetaTags", It.IsAny<Microsoft.AspNetCore.Http.IFormFile>(), It.IsAny<DGE.Domain.Enums.EnumExtensions.FileCategoryEnum>()))
                .ReturnsAsync("/Images/MetaTags/og.jpg");

            using (var context = CreateContext())
            {
                var handler = new MetaTagCommandHandler(_localizer, _mapper, MetaTagService(context), _fileService.Object);
                await handler.Handle(new UpdateMetaTagCommandRequestModel { Page = "Home", Title = "DGE", OgImageFile = new Mock<Microsoft.AspNetCore.Http.IFormFile>().Object }, CancellationToken.None);
            }
            using (var context = CreateContext())
            {
                var handler = new MetaTagCommandHandler(_localizer, _mapper, MetaTagService(context), _fileService.Object);
                await handler.Handle(new UpdateMetaTagCommandRequestModel { Page = "home", Title = "DGE Egypt", Keywords = "fruit,export" }, CancellationToken.None);
            }
            using (var context = CreateContext())
            {
                Assert.Equal(1, await context.Set<MetaTag>().CountAsync());
                var query = new MetaTagHandlerQuery(MetaTagService(context), _localizer, _mapper);
                var home = (await query.Handle(new GetMetaTagByPageQueryRequestModel { Page = "home" }, CancellationToken.None)).Data!;
                Assert.Equal("DGE Egypt", home.Title);
                Assert.Equal("fruit,export", home.Keywords);
                Assert.Equal("/Images/MetaTags/og.jpg", home.OgImage);

                Assert.Equal("contact", (await query.Handle(new GetMetaTagByPageQueryRequestModel { Page = "contact" }, CancellationToken.None)).Data!.Page);
                Assert.Equal(HttpStatusCode.NotFound, (await query.Handle(new GetMetaTagByPageQueryRequestModel { Page = "blog" }, CancellationToken.None)).StatusCode);
            }
        }

        [Fact]
        public async Task CompanySettings_SavesLists_AndHidesNotificationEmailFromAnonymousCallers()
        {
            using (var context = CreateContext())
            {
                var handler = new CompanySettingsCommandHandler(_localizer, _mapper, CompanySettingsService(context));
                await handler.Handle(new UpdateCompanySettingsCommandRequestModel { Phones = ["010", " ", "011 "], Emails = ["a@dge.com"], NotificationEmail = "inbox@dge.com" }, CancellationToken.None);
                await handler.Handle(new UpdateCompanySettingsCommandRequestModel { Phones = ["010", "011"], Emails = ["a@dge.com", "b@dge.com"], Address = "Cairo", NotificationEmail = "inbox@dge.com" }, CancellationToken.None);
            }
            using (var context = CreateContext())
            {
                Assert.Equal(1, await context.Set<CompanySettings>().CountAsync());

                _authenticatedUserService.Setup(s => s.IsAuthenticated()).Returns(false);
                var query = new CompanySettingsHandlerQuery(CompanySettingsService(context), _authenticatedUserService.Object, _localizer, _mapper);
                var anonymous = (await query.Handle(new GetCompanySettingsQueryRequestModel(), CancellationToken.None)).Data!;
                Assert.Equal(["010", "011"], anonymous.Phones);
                Assert.Equal(["a@dge.com", "b@dge.com"], anonymous.Emails);
                Assert.Equal("Cairo", anonymous.Address);
                Assert.Null(anonymous.NotificationEmail);

                _authenticatedUserService.Setup(s => s.IsAuthenticated()).Returns(true);
                Assert.Equal("inbox@dge.com", (await query.Handle(new GetCompanySettingsQueryRequestModel(), CancellationToken.None)).Data!.NotificationEmail);
            }
        }

        [Fact]
        public async Task CompanySettings_Get_ReturnsEmptyBeforeTheFirstSave()
        {
            using var context = CreateContext();
            var response = await new CompanySettingsHandlerQuery(CompanySettingsService(context), _authenticatedUserService.Object, _localizer, _mapper)
                .Handle(new GetCompanySettingsQueryRequestModel(), CancellationToken.None);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(response.Data!.Phones);
        }

        [Fact]
        public async Task Dashboard_CountsSkipDeletedRows_AndSplitUnreadMessages()
        {
            using (var context = CreateContext())
            {
                var category = new ProductCategory { Name = "Fruits" };
                context.Add(category);
                await context.SaveChangesAsync();
                context.AddRange(
                    new Product { Name = "A", ProductCategoryId = category.Id },
                    new Product { Name = "B", ProductCategoryId = category.Id, IsDeleted = true },
                    new Certification { Name = "ISO" },
                    new ContactUs { Name = "1", IsRead = true },
                    new ContactUs { Name = "2" },
                    new ContactUs { Name = "3" });
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var handler = new DashboardHandlerQuery(
                    new ProductService(_localizer, new ProductRepository(context, _localizer), NullLogger<BaseService<Product>>.Instance),
                    new ProductCategoryService(_localizer, new ProductCategoryRepository(context, _localizer), NullLogger<BaseService<ProductCategory>>.Instance),
                    new CertificationService(_localizer, new CertificationRepository(context, _localizer), NullLogger<BaseService<Certification>>.Instance),
                    new ExhibitionService(_localizer, new ExhibitionRepository(context, _localizer), NullLogger<BaseService<Exhibition>>.Instance),
                    new ContactUsService(_localizer, new ContactUsRepository(context, _localizer), NullLogger<BaseService<ContactUs>>.Instance),
                    _localizer);
                var stats = (await handler.Handle(new GetDashboardStatsQueryRequestModel(), CancellationToken.None)).Data!;

                Assert.Equal(1, stats.Products);
                Assert.Equal(1, stats.Categories);
                Assert.Equal(1, stats.Certifications);
                Assert.Equal(0, stats.Exhibitions);
                Assert.Equal(3, stats.Messages);
                Assert.Equal(2, stats.UnreadMessages);
            }
        }

        [Fact]
        public async Task Banner_PaginatedList_FiltersActive_AndOrdersByDisplayOrder()
        {
            using (var context = CreateContext())
            {
                context.AddRange(
                    new Banner { Title = "Third", DisplayOrder = 3 },
                    new Banner { Title = "First", DisplayOrder = 1 },
                    new Banner { Title = "Hidden", DisplayOrder = 2, IsActive = false });
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var query = new BannerHandlerQuery(new BannerService(_localizer, new BannerRepository(context, _localizer), NullLogger<BaseService<Banner>>.Instance), _localizer, _mapper);
                var active = await query.Handle(new GetBannersPaginatedListQueryRequestModel { IsActive = true }, CancellationToken.None);
                var all = await query.Handle(new GetBannersListQueryRequestModel(), CancellationToken.None);

                Assert.Equal(["First", "Third"], active.Data!.Data.Select(b => b.Title));
                Assert.Equal(["First", "Hidden", "Third"], all.Data!.Select(b => b.Title));
            }
        }

        [Fact]
        public async Task SocialMedia_Validator_AcceptsKnownPlatformsAndHttpUrlsOnly()
        {
            using var context = CreateContext();
            var validator = new AddSocialMediaCommandValidator(_localizer,
                new SocialMediaService(_localizer, new SocialMediaRepository(context, _localizer), NullLogger<BaseService<SocialMedia>>.Instance));

            Assert.True((await validator.ValidateAsync(new AddSocialMediaCommandRequestModel { Platform = "Facebook", Url = "https://facebook.com/dge" })).IsValid);
            Assert.Contains((await validator.ValidateAsync(new AddSocialMediaCommandRequestModel { Platform = "myspace", Url = "https://myspace.com" })).Errors, e => e.PropertyName == "Platform");
            Assert.Contains((await validator.ValidateAsync(new AddSocialMediaCommandRequestModel { Platform = "x", Url = "javascript:alert(1)" })).Errors, e => e.PropertyName == "Url");
        }
    }
}
