using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Net;

namespace DGE.Presentation.Shared.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IStringLocalizer<AppLocalization> stringLocalizer, IHostEnvironment environment)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
            {
                _logger.LogInformation("Request {Method} {Path} was cancelled by the client. TraceId: {TraceId}", httpContext.Request.Method, httpContext.Request.Path, traceId);
                if (!httpContext.Response.HasStarted)
                    httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                return true;
            }

            var response = BuildResponse(exception, traceId);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}. TraceId: {TraceId}", httpContext.Request.Method, httpContext.Request.Path, traceId);
            else
                _logger.LogWarning(exception, "Request {Method} {Path} failed with {StatusCode}. TraceId: {TraceId}", httpContext.Request.Method, httpContext.Request.Path, (int)response.StatusCode, traceId);

            if (httpContext.Response.HasStarted)
                return false;

            httpContext.Response.StatusCode = (int)response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }

        private Response<string> BuildResponse(Exception exception, string traceId) => exception switch
        {
            ValidationException e => new Response<string>(string.Join("\n", e.Errors.Select((error, index) => $"{index + 1}) {error.PropertyName}: {error.ErrorMessage}")))
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Errors = e.Errors.Select(error => error.ErrorMessage).ToList()
            },
            UnauthorizedAccessException => new Response<string>(_stringLocalizer[AppLocalizationKeys.UnAuthorized])
            {
                StatusCode = HttpStatusCode.Unauthorized
            },
            KeyNotFoundException => new Response<string>(_stringLocalizer[AppLocalizationKeys.NotFound])
            {
                StatusCode = HttpStatusCode.NotFound
            },
            DbUpdateException => new Response<string>(_stringLocalizer[AppLocalizationKeys.BadRequest])
            {
                StatusCode = HttpStatusCode.BadRequest,
                Meta = new { traceId }
            },
            _ => new Response<string>(_stringLocalizer[AppLocalizationKeys.UnexpectedError])
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = _environment.IsDevelopment() ? [exception.ToString()] : null,
                Meta = new { traceId }
            }
        };
    }
}
