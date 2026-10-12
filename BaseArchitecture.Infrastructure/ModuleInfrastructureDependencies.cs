using BaseArchitecture.Infrastructure.Context.Interceptors;
using BaseArchitecture.Infrastructure.Repository;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.BaseRepository;
using BaseArchitecture.Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Infrastructure
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
