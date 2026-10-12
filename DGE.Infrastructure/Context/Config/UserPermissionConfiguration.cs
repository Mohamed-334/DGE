using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserPermissions)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(up => up.Permission)
                   .WithMany(p => p.UserPermissions)
                   .HasForeignKey(up => up.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(up => up.CanView).HasDefaultValue(false);
            builder.Property(up => up.CanEdit).HasDefaultValue(false);
            builder.Property(up => up.CanAdd).HasDefaultValue(false);
            builder.Property(up => up.CanDelete).HasDefaultValue(false);

            builder.ToTable("UserPermissions");
        }
    }
}
