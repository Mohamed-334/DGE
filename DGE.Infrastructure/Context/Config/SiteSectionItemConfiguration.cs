using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class SiteSectionItemConfiguration : IEntityTypeConfiguration<SiteSectionItem>
    {
        public void Configure(EntityTypeBuilder<SiteSectionItem> builder)
        {
            builder.ToTable("SiteSectionItems");
        }
    }
}
