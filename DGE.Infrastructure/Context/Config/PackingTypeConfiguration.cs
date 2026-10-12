using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class PackingTypeConfiguration : IEntityTypeConfiguration<PackingType>
    {
        public void Configure(EntityTypeBuilder<PackingType> builder)
        {
            builder.ToTable("PackingTypes");
        }
    }
}
