using DGE.Infrastructure.Context;
using DGE.Infrastructure.Shared.Localization;
using DGE.Presentation.Shared.ExceptionHandling;
using DGE.Presentation.Shared.HealthChecks;
using DGE.Tests.TestDoubles;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
using System.Text.Json;

namespace DGE.Tests.Pipeline
{
    public class InfrastructurePipelineTests : IAsyncLifetime
    {
        private readonly SqliteConnection _connection = new("DataSource=:memory:");
        private readonly Mock<IMonitoringApi> _monitoringApi = new();
        private WebApplication _app = null!;
        private HttpClient _client = null!;

        public async Task InitializeAsync()
        {
            _connection.Open();
            _monitoringApi.Setup(m => m.Servers()).Returns([new ServerDto { Name = "server", Heartbeat = DateTime.UtcNow }]);
            var storage = new Mock<JobStorage>();
            storage.Setup(s => s.GetMonitoringApi()).Returns(_monitoringApi.Object);

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = "Production" });
            builder.WebHost.UseTestServer();
            builder.Services.AddSingleton<IStringLocalizer<AppLocalization>, KeyEchoLocalizer>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(_connection));
            builder.Services.AddSingleton(storage.Object);
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddAppHealthChecks();

            _app = builder.Build();
            _app.UseExceptionHandler();
            _app.MapGet("/throw", (HttpContext _) => { throw new InvalidOperationException("Server=prod-sql;Password=secret"); });
            _app.MapAppHealthChecks();

            await _app.StartAsync();
            _client = _app.GetTestClient();
        }

        [Fact]
        public async Task UnhandledException_ReturnsSanitizedResponseContract()
        {
            var response = await _client.GetAsync("/throw");
            var content = await response.Content.ReadAsStringAsync();
            var body = JsonDocument.Parse(content).RootElement;

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.False(body.GetProperty("succeeded").GetBoolean());
            Assert.Equal(AppLocalizationKeys.UnexpectedError, body.GetProperty("message").GetString());
            Assert.DoesNotContain("secret", content);
        }

        [Fact]
        public async Task Live_Returns200WithoutRunningChecks()
        {
            var response = await _client.GetAsync(HealthCheckRoutes.Live);
            var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Healthy", body.GetProperty("status").GetString());
            Assert.Empty(body.GetProperty("checks").EnumerateObject());
        }

        [Fact]
        public async Task Ready_ReportsDatabaseAndHangfire()
        {
            var response = await _client.GetAsync(HealthCheckRoutes.Ready);
            var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Healthy", body.GetProperty("checks").GetProperty("database").GetProperty("status").GetString());
            Assert.Equal("Healthy", body.GetProperty("checks").GetProperty("hangfire").GetProperty("status").GetString());
            Assert.Contains("no-store", response.Headers.CacheControl!.ToString());
        }

        [Fact]
        public async Task Ready_WhenHangfireStorageFails_Returns503WithoutExceptionDetails()
        {
            _monitoringApi.Setup(m => m.Servers()).Throws(new InvalidOperationException("Login failed for user 'sa'"));

            var response = await _client.GetAsync(HealthCheckRoutes.Ready);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.Equal("Unhealthy", JsonDocument.Parse(content).RootElement.GetProperty("status").GetString());
            Assert.DoesNotContain("Login failed", content);
        }

        public async Task DisposeAsync()
        {
            _client.Dispose();
            await _app.DisposeAsync();
            _connection.Dispose();
        }
    }
}
