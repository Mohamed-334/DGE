using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class OtpConfiguration : IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> builder)
        {
            builder.ToTable("Otp");

            // nvarchar(max) columns cannot be indexed; 256 matches Identity's email length
            builder.Property(o => o.Email).HasMaxLength(256);
            builder.Property(o => o.OtpCode).HasMaxLength(16);

            // OTP verification and cleanup filter by Email (+ OtpCode)
            builder.HasIndex(o => new { o.Email, o.OtpCode });
        }
    }
}
