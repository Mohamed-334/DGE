using DGE.Domain.Entities;
using DGE.Domain.Entities.Logger;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Otp> Otp { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductMineral> ProductMinerals { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariety> ProductVarieties { get; set; }
        public DbSet<ProductVitamin> ProductVitamins { get; set; }
        public DbSet<Vitamin> Vitamins { get; set; }
        public DbSet<Mineral> Minerals { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<PackingType> PackingTypes { get; set; }
        public DbSet<ProductPacking> ProductPackings { get; set; }
        public DbSet<ProductHarvestMonth> ProductHarvestMonths { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ContactUs> ContactUsMessages { get; set; }
        public DbSet<Logger> Logger { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
