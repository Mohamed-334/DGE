using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Localization;
using System.Net;

namespace DGE.Presentation.Shared.Csrf
{
    public static class CsrfSettings
    {
        public const string AuthCookieName = "AccessToken";
        public const string HeaderName = "X-CSRF-TOKEN";
        public const string CookieName = "CsrfToken";
    }

    /// <summary>
    /// Browsers attach the AccessToken cookie automatically, so any state-changing request
    /// authenticated by that cookie must also carry a valid antiforgery token in the X-CSRF-TOKEN header.
    /// Requests authenticated with an Authorization header are not exposed to CSRF and are skipped.
    /// </summary>
    public class CsrfProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public CsrfProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAntiforgery antiforgery, IStringLocalizer<AppLocalization> stringLocalizer)
        {
            if (RequiresValidation(context))
            {
                try
                {
                    await antiforgery.ValidateRequestAsync(context);
                }
                catch (AntiforgeryValidationException)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new Response<string>(stringLocalizer[AppLocalizationKeys.InvalidCsrfToken])
                    {
                        StatusCode = HttpStatusCode.Forbidden
                    });
                    return;
                }
            }

            await _next(context);
        }

        private static bool RequiresValidation(HttpContext context)
        {
            var request = context.Request;
            if (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method) ||
                HttpMethods.IsOptions(request.Method) || HttpMethods.IsTrace(request.Method))
                return false;

            if (!request.Cookies.ContainsKey(CsrfSettings.AuthCookieName) || request.Headers.ContainsKey("Authorization"))
                return false;

            // Only controller actions; SignalR and the Hangfire dashboard have their own handling
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is null)
                return false;

            return endpoint.Metadata.GetMetadata<IgnoreAntiforgeryTokenAttribute>() is null;
        }
    }

    public static class CsrfProtectionExtensions
    {
        public static IServiceCollection AddAppCsrfProtection(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = CsrfSettings.HeaderName;
                options.Cookie.Name = CsrfSettings.CookieName;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
            return services;
        }

        public static IApplicationBuilder UseAppCsrfProtection(this IApplicationBuilder app)
            => app.UseMiddleware<CsrfProtectionMiddleware>();
    }
}
