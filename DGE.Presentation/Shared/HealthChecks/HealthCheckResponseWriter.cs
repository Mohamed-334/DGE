using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DGE.Presentation.Shared.HealthChecks
{
    public static class HealthCheckResponseWriter
    {
        public static Task WriteAsync(HttpContext context, HealthReport report)
        {
            context.Response.Headers.CacheControl = "no-store, no-cache";

            var body = new
            {
                status = report.Status.ToString(),
                totalDuration = Math.Round(report.TotalDuration.TotalMilliseconds, 2),
                checks = report.Entries.ToDictionary(
                    entry => entry.Key,
                    entry => new
                    {
                        status = entry.Value.Status.ToString(),
                        duration = Math.Round(entry.Value.Duration.TotalMilliseconds, 2),
                        description = entry.Value.Description,
                        data = entry.Value.Data.Count == 0 ? null : entry.Value.Data
                    })
            };

            return context.Response.WriteAsJsonAsync(body, context.RequestAborted);
        }
    }
}
