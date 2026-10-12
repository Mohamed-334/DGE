using DGE.Infrastructure.Context;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DGE.Presentation.Shared.HealthChecks
{
    public static class HealthCheckTags
    {
        public const string Ready = "ready";
    }

    public static class HealthCheckRoutes
    {
        public const string Health = "/health";
        public const string Ready = "/health/ready";
        public const string Live = "/health/live";
    }

    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            services.TryAddSingleton(TimeProvider.System);
            services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>("database", HealthStatus.Unhealthy, [HealthCheckTags.Ready])
                .AddCheck<HangfireHealthCheck>("hangfire", HealthStatus.Unhealthy, [HealthCheckTags.Ready]);

            return services;
        }

        public static IEndpointRouteBuilder MapAppHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks(HealthCheckRoutes.Health, new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter.WriteAsync
            }).AllowAnonymous().DisableRateLimiting();

            endpoints.MapHealthChecks(HealthCheckRoutes.Ready, new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains(HealthCheckTags.Ready),
                ResponseWriter = HealthCheckResponseWriter.WriteAsync
            }).AllowAnonymous().DisableRateLimiting();

            endpoints.MapHealthChecks(HealthCheckRoutes.Live, new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = HealthCheckResponseWriter.WriteAsync
            }).AllowAnonymous().DisableRateLimiting();

            return endpoints;
        }

        public static bool IsHealthCheckRequest(this HttpContext context)
            => context.Request.Path.StartsWithSegments(HealthCheckRoutes.Health);
    }
}
