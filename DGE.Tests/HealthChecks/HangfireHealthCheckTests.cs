using DGE.Presentation.Shared.HealthChecks;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Time.Testing;
using Moq;

namespace DGE.Tests.HealthChecks
{
    public class HangfireHealthCheckTests
    {
        private static readonly DateTimeOffset Now = new(2026, 9, 28, 12, 0, 0, TimeSpan.Zero);

        private static HealthCheckContext CreateContext(IHealthCheck check) => new()
        {
            Registration = new HealthCheckRegistration("hangfire", check, HealthStatus.Unhealthy, null)
        };

        private static HangfireHealthCheck CreateCheck(params DateTime?[] heartbeats)
        {
            var monitoringApi = new Mock<IMonitoringApi>();
            monitoringApi.Setup(m => m.Servers())
                .Returns(heartbeats.Select((heartbeat, index) => new ServerDto { Name = $"server-{index}", Heartbeat = heartbeat }).ToList());
            var storage = new Mock<JobStorage>();
            storage.Setup(s => s.GetMonitoringApi()).Returns(monitoringApi.Object);
            return new HangfireHealthCheck(storage.Object, new FakeTimeProvider(Now));
        }

        [Fact]
        public async Task ServerWithRecentHeartbeat_IsHealthy()
        {
            var check = CreateCheck(Now.UtcDateTime.AddSeconds(-30));

            var result = await check.CheckHealthAsync(CreateContext(check));

            Assert.Equal(HealthStatus.Healthy, result.Status);
            Assert.Equal(1, result.Data["activeServers"]);
        }

        [Fact]
        public async Task OnlyStaleOrMissingHeartbeats_IsDegraded()
        {
            var check = CreateCheck(Now.UtcDateTime - HangfireHealthCheck.HeartbeatTimeout - TimeSpan.FromSeconds(1), null);

            var result = await check.CheckHealthAsync(CreateContext(check));

            Assert.Equal(HealthStatus.Degraded, result.Status);
            Assert.Equal(0, result.Data["activeServers"]);
        }

        [Fact]
        public async Task NoServers_IsDegraded()
        {
            var check = CreateCheck();

            var result = await check.CheckHealthAsync(CreateContext(check));

            Assert.Equal(HealthStatus.Degraded, result.Status);
        }

        [Fact]
        public async Task StorageFailure_ReturnsRegistrationFailureStatus()
        {
            var storage = new Mock<JobStorage>();
            storage.Setup(s => s.GetMonitoringApi()).Throws(new InvalidOperationException("SQL Server unavailable"));
            var check = new HangfireHealthCheck(storage.Object, new FakeTimeProvider(Now));

            var result = await check.CheckHealthAsync(CreateContext(check));

            Assert.Equal(HealthStatus.Unhealthy, result.Status);
            Assert.IsType<InvalidOperationException>(result.Exception);
        }
    }
}
