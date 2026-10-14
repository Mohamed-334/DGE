using AutoMapper;
using DGE.Core.Features.ContactUsMessages.Commands.Handler;
using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Core.Features.ContactUsMessages.Queries.Handler;
using DGE.Core.Features.ContactUsMessages.Queries.RequestModels;
using DGE.Domain.Entities;
using DGE.Domain.Shared.EmailModels;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.Repository;
using DGE.Service.Service;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using DGE.Tests.TestDoubles;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;

namespace DGE.Tests.Features.ContactUsMessages
{
    public class ContactUsTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly Mock<IAuthenticatedUserService> _authenticatedUserService = new();
        private readonly Mock<IBackgroundJobClient> _backgroundJobClient = new();
        private readonly IMapper _mapper;
        private readonly KeyEchoLocalizer _localizer = new();

        public ContactUsTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            using (var context = CreateContext())
                context.Database.EnsureCreated();

            _authenticatedUserService.Setup(s => s.GetAuthenticatedUserName()).Returns("admin");
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<DGE.Core.Mapping.ContactUsMapping.ContactUsMapping>())
                .CreateMapper(type => Activator.CreateInstance(type, _authenticatedUserService.Object)!);
        }

        public void Dispose() => _connection.Dispose();

        private AppDbContext CreateContext() =>
            new(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options);

        private ContactUsService ContactUsService(AppDbContext context) =>
            new(_localizer, new ContactUsRepository(context, _localizer), NullLogger<BaseService<ContactUs>>.Instance);

        private ContactUsCommandHandler CreateHandler(AppDbContext context, EmailSettings? emailSettings = null) =>
            new(_localizer, _mapper, ContactUsService(context), _authenticatedUserService.Object, _backgroundJobClient.Object,
                emailSettings ?? new EmailSettings { FromEmail = "info@company.com", NotificationEmail = "sales@company.com" },
                new CompanySettingsService(_localizer, new CompanySettingsRepository(context, _localizer), NullLogger<BaseService<CompanySettings>>.Instance),
                NullLogger<ContactUsCommandHandler>.Instance);

        [Fact]
        public async Task Create_SavesMessage_AndQueuesAnEncodedEmailToTheCompany()
        {
            Job? queued = null;
            _backgroundJobClient.Setup(c => c.Create(It.IsAny<Job>(), It.IsAny<IState>()))
                .Callback<Job, IState>((job, _) => queued = job)
                .Returns("1");

            using var context = CreateContext();
            var response = await CreateHandler(context).Handle(new AddContactUsCommandRequestModel
            {
                Name = "<script>x</script>",
                Email = "visitor@mail.com",
                Subject = "Prices",
                Message = "Hello"
            }, CancellationToken.None);

            Assert.True(response.Succeeded);
            var saved = await context.Set<ContactUs>().SingleAsync();
            Assert.False(saved.IsRead);
            Assert.NotNull(saved.CreationDate);

            Assert.NotNull(queued);
            Assert.Equal(nameof(IEmailService.SendEmailAsync), queued!.Method.Name);
            Assert.Equal("sales@company.com", queued.Args[0]);
            Assert.Contains("&lt;script&gt;", (string)queued.Args[1]!);
            Assert.DoesNotContain("<script>", (string)queued.Args[1]!);
        }

        [Fact]
        public async Task Create_StillSucceeds_WhenQueueingTheEmailFails()
        {
            _backgroundJobClient.Setup(c => c.Create(It.IsAny<Job>(), It.IsAny<IState>())).Throws(new InvalidOperationException("storage down"));

            using var context = CreateContext();
            var response = await CreateHandler(context).Handle(new AddContactUsCommandRequestModel
            {
                Name = "A", Email = "a@mail.com", Message = "Hi"
            }, CancellationToken.None);

            Assert.True(response.Succeeded);
            Assert.Equal(1, await context.Set<ContactUs>().CountAsync());
        }

        [Fact]
        public async Task MarkAsRead_SetsTheFlag_AndListFiltersByIt_WithCreatedAt()
        {
            using (var context = CreateContext())
            {
                context.AddRange(
                    new ContactUs { Name = "Old", CreationDate = new DateTime(2026, 1, 1) },
                    new ContactUs { Name = "New", CreationDate = new DateTime(2026, 2, 1) });
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var oldId = await context.Set<ContactUs>().Where(c => c.Name == "Old").Select(c => c.Id).SingleAsync();
                var handler = CreateHandler(context);
                Assert.True((await handler.Handle(new MarkAsReadContactUsCommandRequestModel { Id = oldId }, CancellationToken.None)).Succeeded);
                // Marking twice is harmless
                Assert.True((await handler.Handle(new MarkAsReadContactUsCommandRequestModel { Id = oldId }, CancellationToken.None)).Succeeded);
                Assert.Equal(HttpStatusCode.NotFound, (await handler.Handle(new MarkAsReadContactUsCommandRequestModel { Id = 999 }, CancellationToken.None)).StatusCode);
            }

            using (var context = CreateContext())
            {
                var query = new ContactUsHandlerQuery(ContactUsService(context), _localizer, _mapper);
                var unread = await query.Handle(new GetContactUsMessagesPaginatedListQueryRequestModel { IsRead = false }, CancellationToken.None);
                var all = await query.Handle(new GetContactUsMessagesPaginatedListQueryRequestModel(), CancellationToken.None);

                Assert.Equal(["New"], unread.Data!.Data.Select(c => c.Name));
                // Newest first
                Assert.Equal(["New", "Old"], all.Data!.Data.Select(c => c.Name));
                Assert.Equal(new DateTime(2026, 2, 1), all.Data.Data[0].CreatedAt);
                Assert.True(all.Data.Data[1].IsRead);
            }
        }
    }
}
