using Hangfire.Dashboard;

namespace DGE.Presentation.Shared.Hangfire
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _requiredRole;
        private readonly bool _allowAnonymous;

        public HangfireDashboardAuthorizationFilter(string requiredRole, bool allowAnonymous)
        {
            _requiredRole = requiredRole;
            _allowAnonymous = allowAnonymous;
        }

        public bool Authorize(DashboardContext context)
        {
            if (_allowAnonymous)
                return true;

            var user = context.GetHttpContext().User;
            return user.Identity?.IsAuthenticated == true && user.IsInRole(_requiredRole);
        }
    }
}
