using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace DGE.Presentation.Shared.RateLimiting
{
    public static class RateLimitPolicies
    {
        public const string Auth = "auth-limit";
        public const string OtpResend = "otp-resend-limit";
        public const string PasswordResetRequest = "password-reset-request-limit";
        public const string PasswordReset = "password-reset-limit";
        public const string SignUp = "signup-limit";
        public const string PasswordChange = "password-change-limit";
        public const string OAuth = "oauth-limit";
        public const string EmailSend = "email-send-limit";
        public const string NotificationSend = "notification-send-limit";
    }

    public static class RateLimitingExtensions
    {
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(15);

        public static IServiceCollection AddAppRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy(RateLimitPolicies.Auth, context => FixedWindow(IpKey(context), 5));
                options.AddPolicy(RateLimitPolicies.OtpResend, context => FixedWindow(IpKey(context), 3));
                options.AddPolicy(RateLimitPolicies.PasswordResetRequest, context => FixedWindow(IpKey(context), 5));
                options.AddPolicy(RateLimitPolicies.PasswordReset, context => FixedWindow(IpKey(context), 5));
                options.AddPolicy(RateLimitPolicies.SignUp, context => FixedWindow(IpKey(context), 10));
                options.AddPolicy(RateLimitPolicies.PasswordChange, context => FixedWindow(UserKey(context), 5));
                options.AddPolicy(RateLimitPolicies.OAuth, context => FixedWindow(IpKey(context), 20));
                options.AddPolicy(RateLimitPolicies.EmailSend, context => FixedWindow($"{UserKey(context)}|{IpKey(context)}", 10));
                options.AddPolicy(RateLimitPolicies.NotificationSend, context => FixedWindow(UserKey(context), 20));

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = async (context, cancellationToken) =>
                {
                    var httpContext = context.HttpContext;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                        httpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString(CultureInfo.InvariantCulture);

                    var localizer = httpContext.RequestServices.GetRequiredService<IStringLocalizer<AppLocalization>>();
                    await httpContext.Response.WriteAsJsonAsync(new Response<string>(localizer[AppLocalizationKeys.TooManyRequests])
                    {
                        StatusCode = HttpStatusCode.TooManyRequests
                    }, cancellationToken);
                };
            });

            return services;
        }

        private static RateLimitPartition<string> FixedWindow(string partitionKey, int permitLimit) =>
            RateLimitPartition.GetFixedWindowLimiter(partitionKey, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = permitLimit,
                Window = Window,
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            });

        private static string IpKey(HttpContext context) =>
            $"ip:{context.Connection.RemoteIpAddress?.ToString() ?? "unknown"}";

        // Falls back to the IP for anonymous callers so they still share a limit
        private static string UserKey(HttpContext context)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(userId) ? IpKey(context) : $"user:{userId}";
        }
    }
}
