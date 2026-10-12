using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rp => rp.Permission)
                   .WithMany(p => p.RolePermissions)
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(rp => rp.CanView).HasDefaultValue(false);
            builder.Property(rp => rp.CanEdit).HasDefaultValue(false);
            builder.Property(rp => rp.CanAdd).HasDefaultValue(false);
            builder.Property(rp => rp.CanDelete).HasDefaultValue(false);

            builder.ToTable("RolePermissions");
        }
    }
}
