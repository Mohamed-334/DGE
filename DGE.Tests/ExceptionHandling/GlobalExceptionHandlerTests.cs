using DGE.Infrastructure.Shared.Localization;
using DGE.Presentation.Shared.ExceptionHandling;
using DGE.Tests.TestDoubles;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Text.Json;

namespace DGE.Tests.ExceptionHandling
{
    public class GlobalExceptionHandlerTests
    {
        private static GlobalExceptionHandler CreateHandler(string environmentName = "Production")
        {
            var environment = new Mock<IHostEnvironment>();
            environment.SetupGet(e => e.EnvironmentName).Returns(environmentName);
            return new GlobalExceptionHandler(NullLogger<GlobalExceptionHandler>.Instance, new KeyEchoLocalizer(), environment.Object);
        }

        private static DefaultHttpContext CreateContext()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            return context;
        }

        private static JsonElement ReadBody(HttpContext context)
        {
            context.Response.Body.Position = 0;
            return JsonDocument.Parse(context.Response.Body).RootElement;
        }

        [Fact]
        public async Task ValidationException_Returns422WithErrors()
        {
            var context = CreateContext();
            var exception = new ValidationException([new ValidationFailure("Email", "required"), new ValidationFailure("Password", "too short")]);

            var handled = await CreateHandler().TryHandleAsync(context, exception, CancellationToken.None);

            var body = ReadBody(context);
            Assert.True(handled);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, context.Response.StatusCode);
            Assert.False(body.GetProperty("succeeded").GetBoolean());
            Assert.Equal("1) Email: required\n2) Password: too short", body.GetProperty("message").GetString());
            Assert.Equal(["required", "too short"], body.GetProperty("errors").EnumerateArray().Select(e => e.GetString()));
        }

        [Fact]
        public async Task KeyNotFoundException_Returns404WithLocalizedMessage()
        {
            var context = CreateContext();

            await CreateHandler().TryHandleAsync(context, new KeyNotFoundException("secret internal key"), CancellationToken.None);

            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
            Assert.Equal(AppLocalizationKeys.NotFound, ReadBody(context).GetProperty("message").GetString());
        }

        [Fact]
        public async Task UnauthorizedAccessException_Returns401WithoutLeakingMessage()
        {
            var context = CreateContext();

            await CreateHandler().TryHandleAsync(context, new UnauthorizedAccessException(@"Access to C:\secrets denied"), CancellationToken.None);

            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
            Assert.Equal(AppLocalizationKeys.UnAuthorized, ReadBody(context).GetProperty("message").GetString());
        }

        [Fact]
        public async Task DbUpdateException_Returns400WithoutLeakingDatabaseDetails()
        {
            var context = CreateContext();

            await CreateHandler().TryHandleAsync(context, new DbUpdateException("Violation of UNIQUE KEY constraint 'IX_Users_Email'"), CancellationToken.None);

            var body = ReadBody(context);
            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
            Assert.Equal(AppLocalizationKeys.BadRequest, body.GetProperty("message").GetString());
            Assert.DoesNotContain("IX_Users_Email", body.GetRawText());
        }

        [Fact]
        public async Task UnexpectedException_InProduction_Returns500WithoutDetails()
        {
            var context = CreateContext();

            await CreateHandler("Production").TryHandleAsync(context, new InvalidOperationException("connection string Password=hunter2"), CancellationToken.None);

            var body = ReadBody(context);
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            Assert.Equal(AppLocalizationKeys.UnexpectedError, body.GetProperty("message").GetString());
            Assert.Equal(JsonValueKind.Null, body.GetProperty("errors").ValueKind);
            Assert.DoesNotContain("hunter2", body.GetRawText());
            Assert.False(string.IsNullOrEmpty(body.GetProperty("meta").GetProperty("traceId").GetString()));
        }

        [Fact]
        public async Task UnexpectedException_InDevelopment_IncludesExceptionDetails()
        {
            var context = CreateContext();

            await CreateHandler("Development").TryHandleAsync(context, new InvalidOperationException("boom"), CancellationToken.None);

            var errors = ReadBody(context).GetProperty("errors").EnumerateArray().Select(e => e.GetString()).ToList();
            Assert.Contains(errors, error => error!.Contains("boom"));
        }

        [Fact]
        public async Task CancelledByClient_Returns499WithoutBody()
        {
            using var aborted = new CancellationTokenSource();
            aborted.Cancel();
            var context = CreateContext();
            context.RequestAborted = aborted.Token;

            var handled = await CreateHandler().TryHandleAsync(context, new OperationCanceledException(), CancellationToken.None);

            Assert.True(handled);
            Assert.Equal(StatusCodes.Status499ClientClosedRequest, context.Response.StatusCode);
            Assert.Equal(0, context.Response.Body.Length);
        }

        [Fact]
        public async Task OperationCanceled_WithoutClientAbort_IsTreatedAsServerError()
        {
            var context = CreateContext();

            await CreateHandler().TryHandleAsync(context, new TaskCanceledException("timeout"), CancellationToken.None);

            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }
    }
}
