using DGE.Infrastructure.Shared.Localization;
using DGE.Presentation.Shared.Csrf;
using DGE.Tests.TestDoubles;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;
using System.Text.Json;

namespace DGE.Tests.Csrf
{
    public class CsrfProtectionMiddlewareTests
    {
        private readonly Mock<IAntiforgery> _antiforgery = new();

        public CsrfProtectionMiddlewareTests()
        {
            _antiforgery.Setup(a => a.ValidateRequestAsync(It.IsAny<HttpContext>()))
                .ThrowsAsync(new AntiforgeryValidationException("invalid"));
        }

        private static DefaultHttpContext CreateContext(string method, bool withAuthCookie = true, bool withAuthorizationHeader = false, bool controllerEndpoint = true, bool ignoreAntiforgery = false)
        {
            var context = new DefaultHttpContext();
            context.Request.Method = method;
            context.Response.Body = new MemoryStream();
            if (withAuthCookie)
                context.Request.Headers.Cookie = $"{CsrfSettings.AuthCookieName}=token";
            if (withAuthorizationHeader)
                context.Request.Headers.Authorization = "Bearer token";

            var metadata = new List<object>();
            if (controllerEndpoint)
                metadata.Add(new ControllerActionDescriptor());
            if (ignoreAntiforgery)
                metadata.Add(new IgnoreAntiforgeryTokenAttribute());
            context.SetEndpoint(new Endpoint(null, new EndpointMetadataCollection(metadata), "test"));
            return context;
        }

        private async Task<bool> InvokeAsync(HttpContext context)
        {
            var nextCalled = false;
            var middleware = new CsrfProtectionMiddleware(_ =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            });
            await middleware.Invoke(context, _antiforgery.Object, new KeyEchoLocalizer());
            return nextCalled;
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("HEAD")]
        [InlineData("OPTIONS")]
        public async Task SafeMethods_AreNotValidated(string method)
        {
            Assert.True(await InvokeAsync(CreateContext(method)));
            _antiforgery.Verify(a => a.ValidateRequestAsync(It.IsAny<HttpContext>()), Times.Never);
        }

        [Fact]
        public async Task UnsafeMethodWithoutCookie_IsNotValidated()
        {
            Assert.True(await InvokeAsync(CreateContext("POST", withAuthCookie: false)));
        }

        [Fact]
        public async Task UnsafeMethodWithAuthorizationHeader_IsNotValidated()
        {
            Assert.True(await InvokeAsync(CreateContext("POST", withAuthorizationHeader: true)));
        }

        [Fact]
        public async Task NonControllerEndpoint_IsNotValidated()
        {
            Assert.True(await InvokeAsync(CreateContext("POST", controllerEndpoint: false)));
        }

        [Fact]
        public async Task EndpointWithIgnoreAntiforgery_IsNotValidated()
        {
            Assert.True(await InvokeAsync(CreateContext("POST", ignoreAntiforgery: true)));
        }

        [Fact]
        public async Task CookieAuthenticatedPostWithInvalidToken_Returns403()
        {
            var context = CreateContext("POST");

            var nextCalled = await InvokeAsync(context);

            Assert.False(nextCalled);
            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            context.Response.Body.Position = 0;
            var body = JsonDocument.Parse(context.Response.Body).RootElement;
            Assert.Equal(AppLocalizationKeys.InvalidCsrfToken, body.GetProperty("message").GetString());
        }

        [Fact]
        public async Task CookieAuthenticatedPostWithValidToken_CallsNext()
        {
            _antiforgery.Setup(a => a.ValidateRequestAsync(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            Assert.True(await InvokeAsync(CreateContext("DELETE")));
        }
    }
}
