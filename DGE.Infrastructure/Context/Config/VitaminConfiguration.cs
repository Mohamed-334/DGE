using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class VitaminConfiguration : IEntityTypeConfiguration<Vitamin>
    {
        public void Configure(EntityTypeBuilder<Vitamin> builder)
        {
            builder.ToTable("Vitamins");
        }
    }
}
