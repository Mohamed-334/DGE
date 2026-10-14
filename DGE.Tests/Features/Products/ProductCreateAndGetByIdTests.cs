using AutoMapper;
using DGE.Core.Features.Products.Commands.Handler;
using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Features.Products.Commands.Validators;
using DGE.Core.Features.Products.Dto;
using DGE.Core.Features.Products.Queries.Handler;
using DGE.Core.Features.Products.Queries.RequestModels;
using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.Repository;
using DGE.Service.Service;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using DGE.Tests.TestDoubles;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;
using static DGE.Domain.Enums.EnumExtensions;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Tests.Features.Products
{
    public class ProductCreateAndGetByIdTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly Mock<IFileService> _fileService = new();
        private readonly Mock<IAuthenticatedUserService> _authenticatedUserService = new();
        private readonly IMapper _mapper;
        private readonly KeyEchoLocalizer _localizer = new();

        public ProductCreateAndGetByIdTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            using (var context = CreateContext())
                context.Database.EnsureCreated();

            _authenticatedUserService.Setup(s => s.GetAuthenticatedUserName()).Returns("tester");
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<DGE.Core.Mapping.ProductMapping.ProductMapping>())
                .CreateMapper(type => Activator.CreateInstance(type, _authenticatedUserService.Object)!);
        }

        public void Dispose() => _connection.Dispose();

        private AppDbContext CreateContext() =>
            new(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options);

        private ProductService ProductService(AppDbContext context) =>
            new(_localizer, new ProductRepository(context, _localizer), NullLogger<BaseService<Product>>.Instance);

        private ProductCommandHandler CreateHandler(AppDbContext context) =>
            new(_localizer, _mapper, ProductService(context), _authenticatedUserService.Object, _fileService.Object,
                new TypeService(_localizer, new TypeRepository(context, _localizer), NullLogger<BaseService<Type>>.Instance),
                new PackingTypeService(_localizer, new PackingTypeRepository(context, _localizer), NullLogger<BaseService<PackingType>>.Instance));

        private AddProductCommandValidator CreateValidator(AppDbContext context) =>
            new(_localizer, ProductService(context),
                new VitaminService(_localizer, new VitaminRepository(context, _localizer), NullLogger<BaseService<Vitamin>>.Instance),
                new MineralService(_localizer, new MineralRepository(context, _localizer), NullLogger<BaseService<Mineral>>.Instance));

        private static IFormFile FakeImage(string name)
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns(name);
            file.Setup(f => f.Length).Returns(10);
            return file.Object;
        }

        private async Task<(int CategoryId, int TypeId, int PackingTypeId, int VitaminId, int MineralId)> SeedAsync()
        {
            using var context = CreateContext();
            var category = new ProductCategory { Name = "Fruits" };
            var type = new Type { Name = "Fresh" };
            var packingType = new PackingType { Name = "Box" };
            var vitamin = new Vitamin { Name = "Vitamin C" };
            var mineral = new Mineral { Name = "Iron" };
            context.AddRange(category, type, packingType, vitamin, mineral);
            await context.SaveChangesAsync();
            return (category.Id, type.Id, packingType.Id, vitamin.Id, mineral.Id);
        }

        [Fact]
        public async Task Create_WithAllLists_SavesEverything_AndGetByIdReturnsIt()
        {
            var seed = await SeedAsync();
            _fileService.SetupSequence(s => s.UploadFileAsync("Products", It.IsAny<IFormFile>(), FileCategoryEnum.Image))
                .ReturnsAsync("/Images/Products/a.jpg")
                .ReturnsAsync("/Images/Products/b.jpg");

            var request = new AddProductCommandRequestModel
            {
                Name = "Mango",
                Description = "Sweet mango",
                ProductCategoryId = seed.CategoryId,
                Images = [FakeImage("a.jpg"), FakeImage("b.jpg")],
                VarietyNames = ["Keitt", " keitt ", "Alphonso", " "],
                TypeNames = ["fresh", "Organic"],
                PackingTypeNames = ["BOX", "Bag"],
                VitaminIds = [seed.VitaminId, seed.VitaminId],
                MineralIds = [seed.MineralId],
                HarvestMonthCodes = [1, 1, 6]
            };

            int productId;
            using (var context = CreateContext())
            {
                var response = await CreateHandler(context).Handle(request, CancellationToken.None);
                Assert.True(response.Succeeded);
                productId = (int)response.Meta!.GetType().GetProperty("Id")!.GetValue(response.Meta)!;
            }

            using (var context = CreateContext())
            {
                // Existing names are reused (ignoring case); only the missing ones are created
                Assert.Equal(["Fresh", "Organic"], await context.Set<Type>().OrderBy(t => t.Id).Select(t => t.Name).ToListAsync());
                Assert.Equal(["Box", "Bag"], await context.Set<PackingType>().OrderBy(p => p.Id).Select(p => p.Name).ToListAsync());

                var response = await new ProductHandlerQuery(ProductService(context), _localizer, _mapper)
                    .Handle(new GetProductByIdQueryRequestModel { Id = productId }, CancellationToken.None);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var dto = response.Data!;
                Assert.Equal("Mango", dto.Name);
                Assert.Equal("Fruits", dto.ProductCategoryName);
                Assert.Equal(["/Images/Products/a.jpg", "/Images/Products/b.jpg"], dto.Images!.Select(i => i.ImagePath).Order());
                Assert.Equal(["Alphonso", "Keitt"], dto.Varieties!.Select(v => v.Name).Order());
                Assert.Equal(["Fresh", "Organic"], dto.Types!.Select(t => t.Name).Order());
                Assert.Contains(dto.Types!, t => t.Id == seed.TypeId && t.Name == "Fresh");
                Assert.Equal(["Bag", "Box"], dto.PackingTypes!.Select(p => p.Name).Order());
                Assert.Contains(dto.PackingTypes!, p => p.Id == seed.PackingTypeId);
                Assert.Equal([seed.VitaminId], dto.Vitamins!.Select(v => v.Id));
                Assert.Equal("Vitamin C", dto.Vitamins![0].Name);
                Assert.Equal([seed.MineralId], dto.Minerals!.Select(m => m.Id));
                Assert.Equal([1, 6], dto.HarvestMonths!.Select(h => h.Code!.Value).Order());
                Assert.Contains(dto.HarvestMonths!, h => h.Code == 1 && h.Month == "Jan" && h.DisplayName == "January");
            }
        }

        [Fact]
        public async Task Create_WithoutLists_SavesProductOnly()
        {
            var seed = await SeedAsync();

            using var context = CreateContext();
            var response = await CreateHandler(context).Handle(
                new AddProductCommandRequestModel { Name = "Guava", ProductCategoryId = seed.CategoryId }, CancellationToken.None);

            Assert.True(response.Succeeded);
            Assert.Equal(1, await context.Set<Product>().CountAsync());
            Assert.Equal(0, await context.Set<ProductImage>().CountAsync());
            Assert.Equal(1, await context.Set<Type>().CountAsync());
            _fileService.Verify(s => s.UploadFileAsync(It.IsAny<string>(), It.IsAny<IFormFile>(), It.IsAny<FileCategoryEnum>()), Times.Never);
        }

        [Fact]
        public async Task Create_WhenImageUploadFails_ReturnsBadRequestAndSavesNothing()
        {
            var seed = await SeedAsync();
            _fileService.Setup(s => s.UploadFileAsync("Products", It.IsAny<IFormFile>(), FileCategoryEnum.Image))
                .ReturnsAsync("FailedToUploadImage");

            using var context = CreateContext();
            var response = await CreateHandler(context).Handle(new AddProductCommandRequestModel
            {
                Name = "Mango",
                ProductCategoryId = seed.CategoryId,
                Images = [FakeImage("a.jpg")],
                TypeNames = ["Organic"]
            }, CancellationToken.None);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(0, await context.Set<Product>().CountAsync());
            Assert.Equal(1, await context.Set<Type>().CountAsync());
        }

        [Fact]
        public async Task GetById_ReturnsLinkIdsNextToItemIds()
        {
            var seed = await SeedAsync();
            int productId;
            using (var context = CreateContext())
            {
                var response = await CreateHandler(context).Handle(new AddProductCommandRequestModel
                {
                    Name = "Mango",
                    ProductCategoryId = seed.CategoryId,
                    VarietyNames = ["Keitt"],
                    VitaminIds = [seed.VitaminId],
                    HarvestMonthCodes = [3]
                }, CancellationToken.None);
                productId = (int)response.Meta!.GetType().GetProperty("Id")!.GetValue(response.Meta)!;
            }

            using (var context = CreateContext())
            {
                var linkVitaminId = await context.Set<ProductVitamin>().Select(v => v.Id).SingleAsync();
                var varietyId = await context.Set<ProductVariety>().Select(v => v.Id).SingleAsync();
                var harvestMonthId = await context.Set<ProductHarvestMonth>().Select(h => h.Id).SingleAsync();

                var dto = (await new ProductHandlerQuery(ProductService(context), _localizer, _mapper)
                    .Handle(new GetProductByIdQueryRequestModel { Id = productId }, CancellationToken.None)).Data!;

                Assert.Equal(seed.VitaminId, dto.Vitamins![0].Id);
                Assert.Equal(linkVitaminId, dto.Vitamins![0].LinkId);
                Assert.Equal(varietyId, dto.Varieties![0].LinkId);
                Assert.Equal(harvestMonthId, dto.HarvestMonths![0].Id);
            }
        }

        [Fact]
        public async Task Create_GeneratesUniqueSlugs_AndKeepsAProvidedOne()
        {
            var seed = await SeedAsync();
            using var context = CreateContext();
            var handler = CreateHandler(context);

            await handler.Handle(new AddProductCommandRequestModel { Name = "Fresh Mango", ProductCategoryId = seed.CategoryId }, CancellationToken.None);
            await handler.Handle(new AddProductCommandRequestModel { Name = "fresh  mango!", ProductCategoryId = seed.CategoryId }, CancellationToken.None);
            await handler.Handle(new AddProductCommandRequestModel { Name = "مانجو", ProductCategoryId = seed.CategoryId }, CancellationToken.None);
            await handler.Handle(new AddProductCommandRequestModel { Name = "Guava", Slug = "my-guava", ProductCategoryId = seed.CategoryId }, CancellationToken.None);

            Assert.Equal(["fresh-mango", "fresh-mango-2", "product", "my-guava"],
                await context.Set<Product>().OrderBy(p => p.Id).Select(p => p.Slug).ToListAsync());
        }

        [Fact]
        public async Task Update_WithoutSlugOrHomeFields_KeepsThem()
        {
            var seed = await SeedAsync();
            int productId;
            using (var context = CreateContext())
            {
                var response = await CreateHandler(context).Handle(new AddProductCommandRequestModel
                {
                    Name = "Mango", ProductCategoryId = seed.CategoryId, ShowOnHome = true, DisplayOrder = 4, Slug = "mango"
                }, CancellationToken.None);
                productId = (int)response.Meta!.GetType().GetProperty("Id")!.GetValue(response.Meta)!;
            }

            using (var context = CreateContext())
            {
                var response = await CreateHandler(context).Handle(new UpdateProductCommandRequestModel
                {
                    Id = productId, Name = "Mango Keitt", ProductCategoryId = seed.CategoryId
                }, CancellationToken.None);
                Assert.True(response.Succeeded);
            }

            using (var context = CreateContext())
            {
                var product = await context.Set<Product>().SingleAsync();
                Assert.Equal("Mango Keitt", product.Name);
                Assert.Equal("mango", product.Slug);
                Assert.True(product.ShowOnHome);
                Assert.Equal(4, product.DisplayOrder);
            }
        }

        [Fact]
        public async Task ToggleShowOnHome_FlipsTheFlag_AndPaginatedListFiltersAndOrdersByDisplayOrder()
        {
            var seed = await SeedAsync();
            using (var context = CreateContext())
            {
                context.AddRange(
                    new Product { Name = "A", ProductCategoryId = seed.CategoryId, DisplayOrder = 3 },
                    new Product { Name = "B", ProductCategoryId = seed.CategoryId, DisplayOrder = 1 },
                    new Product { Name = "C", ProductCategoryId = seed.CategoryId, DisplayOrder = 2 });
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var handler = CreateHandler(context);
                var ids = await context.Set<Product>().Where(p => p.Name != "C").Select(p => p.Id).ToListAsync();
                foreach (var id in ids)
                    Assert.True((await handler.Handle(new ToggleShowOnHomeProductCommandRequestModel { Id = id }, CancellationToken.None)).Succeeded);

                var notFound = await handler.Handle(new ToggleShowOnHomeProductCommandRequestModel { Id = 999 }, CancellationToken.None);
                Assert.Equal(HttpStatusCode.NotFound, notFound.StatusCode);
            }

            using (var context = CreateContext())
            {
                var query = new ProductHandlerQuery(ProductService(context), _localizer, _mapper);
                var onHome = await query.Handle(new GetProductsPaginatedListQueryRequestModel { ShowOnHome = true }, CancellationToken.None);
                var all = await query.Handle(new GetProductsPaginatedListQueryRequestModel(), CancellationToken.None);

                Assert.Equal(["B", "A"], onHome.Data!.Data.Select(p => p.Name));
                Assert.Equal(["B", "C", "A"], all.Data!.Data.Select(p => p.Name));
            }
        }

        [Fact]
        public async Task GetBySlug_ReturnsTheProduct_OrNotFound()
        {
            var seed = await SeedAsync();
            using (var context = CreateContext())
                await CreateHandler(context).Handle(new AddProductCommandRequestModel { Name = "Mango", ProductCategoryId = seed.CategoryId }, CancellationToken.None);

            using (var context = CreateContext())
            {
                var query = new ProductHandlerQuery(ProductService(context), _localizer, _mapper);
                var found = await query.Handle(new GetProductBySlugQueryRequestModel { Slug = "mango" }, CancellationToken.None);
                var missing = await query.Handle(new GetProductBySlugQueryRequestModel { Slug = "nope" }, CancellationToken.None);

                Assert.Equal("Mango", found.Data!.Name);
                Assert.Equal(HttpStatusCode.NotFound, missing.StatusCode);
            }
        }

        [Fact]
        public async Task Validators_RejectInvalidOrTakenSlugs()
        {
            var seed = await SeedAsync();
            using var context = CreateContext();
            context.Add(new Product { Name = "Mango", Slug = "mango", ProductCategoryId = seed.CategoryId });
            await context.SaveChangesAsync();
            var mangoId = await context.Set<Product>().Select(p => p.Id).SingleAsync();

            var addValidator = CreateValidator(context);
            var updateValidator = new UpdateProductCommandValidator(_localizer, ProductService(context));

            Assert.Contains((await addValidator.ValidateAsync(new AddProductCommandRequestModel { Name = "X", ProductCategoryId = seed.CategoryId, Slug = "mango" })).Errors, e => e.PropertyName == "Slug");
            Assert.Contains((await addValidator.ValidateAsync(new AddProductCommandRequestModel { Name = "X", ProductCategoryId = seed.CategoryId, Slug = "Bad Slug" })).Errors, e => e.PropertyName == "Slug");
            Assert.True((await addValidator.ValidateAsync(new AddProductCommandRequestModel { Name = "X", ProductCategoryId = seed.CategoryId, Slug = "new-one-2" })).IsValid);
            // Re-sending its own slug on update is fine; taking it for another product is not
            Assert.True((await updateValidator.ValidateAsync(new UpdateProductCommandRequestModel { Id = mangoId, Name = "Mango", ProductCategoryId = seed.CategoryId, Slug = "mango" })).IsValid);
            Assert.False((await updateValidator.ValidateAsync(new UpdateProductCommandRequestModel { Id = mangoId + 1, Name = "Y", ProductCategoryId = seed.CategoryId, Slug = "mango" })).IsValid);
        }

        [Fact]
        public void Mapping_WithoutLoadedCollections_LeavesListsNull()
        {
            var dto = _mapper.Map<ProductFullDataDto>(new Product { Id = 1, Name = "Mango", ProductCategoryId = 1 });

            Assert.Null(dto.Images);
            Assert.Null(dto.Types);
            Assert.Null(dto.HarvestMonths);
        }

        [Fact]
        public async Task Validator_RejectsUnknownIdsAndInvalidMonthCodes()
        {
            var seed = await SeedAsync();
            using var context = CreateContext();
            var validator = CreateValidator(context);

            var valid = await validator.ValidateAsync(new AddProductCommandRequestModel
            {
                Name = "Mango",
                ProductCategoryId = seed.CategoryId,
                VitaminIds = [seed.VitaminId],
                MineralIds = [seed.MineralId],
                HarvestMonthCodes = [1, 12]
            });
            var invalid = await validator.ValidateAsync(new AddProductCommandRequestModel
            {
                Name = "Mango",
                ProductCategoryId = seed.CategoryId,
                VitaminIds = [seed.VitaminId, 999],
                MineralIds = [999],
                HarvestMonthCodes = [0, 13]
            });

            Assert.True(valid.IsValid);
            Assert.Contains(invalid.Errors, e => e.PropertyName == "VitaminIds");
            Assert.Contains(invalid.Errors, e => e.PropertyName == "MineralIds");
            Assert.Equal(2, invalid.Errors.Count(e => e.PropertyName.StartsWith("HarvestMonthCodes")));
        }
    }
}
