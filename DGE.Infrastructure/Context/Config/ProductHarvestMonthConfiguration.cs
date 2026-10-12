using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductHarvestMonthConfiguration : IEntityTypeConfiguration<ProductHarvestMonth>
    {
        public void Configure(EntityTypeBuilder<ProductHarvestMonth> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(p2 => p2.ProductHarvestMonths)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductHarvestMonths");
        }
    }
}
