using DGE.Domain.Shared.CaptchaModels;
using DGE.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Text;

namespace DGE.Tests.Security
{
    public class CaptchaServiceTests
    {
        private sealed class StubHandler(HttpStatusCode status, string body) : HttpMessageHandler
        {
            public string? SentForm { get; private set; }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                SentForm = await request.Content!.ReadAsStringAsync(cancellationToken);
                return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            }
        }

        private static CaptchaService Create(CaptchaSettings settings, StubHandler handler) =>
            new(new HttpClient(handler), settings, new HttpContextAccessor(), NullLogger<CaptchaService>.Instance);

        private static CaptchaSettings Enabled() => new() { Enabled = true, SecretKey = "secret", MinimumScore = 0.5 };

        [Fact]
        public async Task Disabled_AcceptsWithoutCallingTheProvider()
        {
            var handler = new StubHandler(HttpStatusCode.OK, "{\"success\":false}");
            Assert.True(await Create(new CaptchaSettings { Enabled = false }, handler).VerifyAsync(null));
            Assert.Null(handler.SentForm);
        }

        [Fact]
        public async Task EnabledWithoutSecret_OrMissingToken_Rejects()
        {
            var handler = new StubHandler(HttpStatusCode.OK, "{\"success\":true}");
            Assert.False(await Create(new CaptchaSettings { Enabled = true }, handler).VerifyAsync("token"));
            Assert.False(await Create(Enabled(), handler).VerifyAsync(" "));
            Assert.Null(handler.SentForm);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK, "{\"success\":true}", true)]
        [InlineData(HttpStatusCode.OK, "{\"success\":true,\"score\":0.9}", true)]
        [InlineData(HttpStatusCode.OK, "{\"success\":true,\"score\":0.1}", false)]
        [InlineData(HttpStatusCode.OK, "{\"success\":false}", false)]
        [InlineData(HttpStatusCode.OK, "not json", false)]
        [InlineData(HttpStatusCode.InternalServerError, "{\"success\":true}", false)]
        public async Task Verify_UsesTheProviderResult(HttpStatusCode status, string body, bool expected)
        {
            var handler = new StubHandler(status, body);
            Assert.Equal(expected, await Create(Enabled(), handler).VerifyAsync("token"));
            Assert.Contains("secret=secret", handler.SentForm);
            Assert.Contains("response=token", handler.SentForm);
        }
    }
}
