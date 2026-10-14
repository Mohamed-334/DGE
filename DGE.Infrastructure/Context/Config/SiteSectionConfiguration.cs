using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class SiteSectionConfiguration : IEntityTypeConfiguration<SiteSection>
    {
        public void Configure(EntityTypeBuilder<SiteSection> builder)
        {
            builder.Property(s => s.Key).HasMaxLength(50);
            builder.HasIndex(s => s.Key)
                   .IsUnique()
                   .HasFilter("[Key] IS NOT NULL");

            builder.HasMany(s => s.Items)
                   .WithOne(i => i.SiteSection)
                   .HasForeignKey(i => i.SiteSectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("SiteSections");
        }
    }
}
