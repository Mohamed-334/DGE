using DGE.Presentation.Shared.Hangfire;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;

namespace DGE.Tests.Hangfire
{
    public class HangfireDashboardAuthorizationFilterTests
    {
        private static DashboardContext CreateContext(ClaimsPrincipal user)
        {
            var httpContext = new DefaultHttpContext { User = user, RequestServices = new ServiceCollection().BuildServiceProvider() };
            return new AspNetCoreDashboardContext(new Mock<JobStorage>().Object, new DashboardOptions(), httpContext);
        }

        private static ClaimsPrincipal Authenticated(params string[] roles)
            => new(new ClaimsIdentity(roles.Select(role => new Claim(ClaimTypes.Role, role)).Append(new Claim(ClaimTypes.Name, "user")), "Bearer"));

        [Fact]
        public void AllowAnonymous_AuthorizesAnonymousUser()
        {
            var filter = new HangfireDashboardAuthorizationFilter("Admin", allowAnonymous: true);

            Assert.True(filter.Authorize(CreateContext(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        [Fact]
        public void AnonymousUser_IsRejected()
        {
            var filter = new HangfireDashboardAuthorizationFilter("Admin", allowAnonymous: false);

            Assert.False(filter.Authorize(CreateContext(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        [Fact]
        public void AuthenticatedUserWithoutRole_IsRejected()
        {
            var filter = new HangfireDashboardAuthorizationFilter("Admin", allowAnonymous: false);

            Assert.False(filter.Authorize(CreateContext(Authenticated("User"))));
        }

        [Fact]
        public void AuthenticatedAdmin_IsAuthorized()
        {
            var filter = new HangfireDashboardAuthorizationFilter("Admin", allowAnonymous: false);

            Assert.True(filter.Authorize(CreateContext(Authenticated("Admin"))));
        }
    }
}
