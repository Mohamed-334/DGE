using DGE.Infrastructure.Context.Interceptors;
using DGE.Infrastructure.Repository;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DGE.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection Services)
        {
            // Register service dependencies here
            // Example: services.AddScoped<IMyService, MyService>();
            Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            Services.AddScoped(typeof(IOtpRepository), typeof(OtpRepository));
            Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
            Services.AddScoped(typeof(IUserNotificationRepository), typeof(UserNotificationRepository));
            Services.AddScoped(typeof(IPermissionRepository), typeof(PermissionRepository));
            Services.AddScoped(typeof(IRolePermissionRepository), typeof(RolePermissionRepository));
            Services.AddScoped(typeof(IUserPermissionRepository), typeof(UserPermissionRepository));
            Services.AddScoped<LoggerSaveChangesInterceptor>();
            return Services;
        }
    }
}
