using DGE.Service.Service;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.Email;
using Microsoft.Extensions.DependencyInjection;

namespace DGE.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            // Register service dependencies here
            // Example: services.AddScoped<IMyService, MyService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<SmtpConnection>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IProductMineralService, ProductMineralService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductVarietyService, ProductVarietyService>();
            services.AddScoped<IProductVitaminService, ProductVitaminService>();
            services.AddScoped<IVitaminService, VitaminService>();
            services.AddScoped<IMineralService, MineralService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IPackingTypeService, PackingTypeService>();
            services.AddScoped<IProductPackingService, ProductPackingService>();
            services.AddScoped<IProductHarvestMonthService, ProductHarvestMonthService>();
            services.AddScoped<ICertificationService, CertificationService>();
            services.AddScoped<IExhibitionService, ExhibitionService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            return services;
        }
    }
}
