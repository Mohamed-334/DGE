using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class MetaTagConfiguration : IEntityTypeConfiguration<MetaTag>
    {
        public void Configure(EntityTypeBuilder<MetaTag> builder)
        {
            builder.Property(m => m.Page).HasMaxLength(50);
            builder.HasIndex(m => m.Page)
                   .IsUnique()
                   .HasFilter("[Page] IS NOT NULL");

            builder.ToTable("MetaTags");
        }
    }
}
