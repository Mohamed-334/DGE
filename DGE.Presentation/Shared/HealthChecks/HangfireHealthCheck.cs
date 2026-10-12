using Hangfire;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DGE.Presentation.Shared.HealthChecks
{
    public class HangfireHealthCheck : IHealthCheck
    {
        public static readonly TimeSpan HeartbeatTimeout = TimeSpan.FromMinutes(5);

        private readonly JobStorage _jobStorage;
        private readonly TimeProvider _timeProvider;

        public HangfireHealthCheck(JobStorage jobStorage, TimeProvider timeProvider)
        {
            _jobStorage = jobStorage;
            _timeProvider = timeProvider;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var servers = await Task.Run(() => _jobStorage.GetMonitoringApi().Servers(), cancellationToken);
                var now = _timeProvider.GetUtcNow().UtcDateTime;
                var activeServers = servers.Count(server => server.Heartbeat.HasValue && now - server.Heartbeat.Value <= HeartbeatTimeout);

                var data = new Dictionary<string, object> { ["activeServers"] = activeServers };
                return activeServers > 0
                    ? HealthCheckResult.Healthy(data: data)
                    : HealthCheckResult.Degraded("No active Hangfire server; background jobs are not being processed.", data: data);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, "Hangfire storage is unreachable.", ex);
            }
        }
    }
}
