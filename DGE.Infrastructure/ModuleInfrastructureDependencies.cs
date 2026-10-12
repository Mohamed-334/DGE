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
            Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            Services.AddScoped(typeof(IProductCategoryRepository), typeof(ProductCategoryRepository));
            Services.AddScoped(typeof(IProductImageRepository), typeof(ProductImageRepository));
            Services.AddScoped(typeof(IProductMineralRepository), typeof(ProductMineralRepository));
            Services.AddScoped(typeof(IProductTypeRepository), typeof(ProductTypeRepository));
            Services.AddScoped(typeof(IProductVarietyRepository), typeof(ProductVarietyRepository));
            Services.AddScoped(typeof(IProductVitaminRepository), typeof(ProductVitaminRepository));
            Services.AddScoped(typeof(IVitaminRepository), typeof(VitaminRepository));
            Services.AddScoped(typeof(IMineralRepository), typeof(MineralRepository));
            Services.AddScoped(typeof(ITypeRepository), typeof(TypeRepository));
            Services.AddScoped(typeof(IPackingTypeRepository), typeof(PackingTypeRepository));
            Services.AddScoped(typeof(IProductPackingRepository), typeof(ProductPackingRepository));
            Services.AddScoped(typeof(IProductHarvestMonthRepository), typeof(ProductHarvestMonthRepository));
            Services.AddScoped(typeof(ICertificationRepository), typeof(CertificationRepository));
            Services.AddScoped(typeof(IExhibitionRepository), typeof(ExhibitionRepository));
            Services.AddScoped(typeof(IContactUsRepository), typeof(ContactUsRepository));
            Services.AddScoped<LoggerSaveChangesInterceptor>();
            return Services;
        }
    }
}
