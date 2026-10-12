using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductMineralConfiguration : IEntityTypeConfiguration<ProductMineral>
    {
        public void Configure(EntityTypeBuilder<ProductMineral> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(p2 => p2.ProductMinerals)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Mineral)
                   .WithMany(m => m.ProductMinerals)
                   .HasForeignKey(p => p.MineralId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductMinerals");
        }
    }
}
