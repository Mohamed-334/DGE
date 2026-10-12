using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Context.Config
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.ToTable("UserNotifications");

            // Unread lookups filter by UserId + IsRead; also covers the plain UserId foreign-key lookups
            builder.HasIndex(un => new { un.UserId, un.IsRead });
        }
    }
}
