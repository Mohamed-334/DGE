using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductPackingConfiguration : IEntityTypeConfiguration<ProductPacking>
    {
        public void Configure(EntityTypeBuilder<ProductPacking> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(p2 => p2.ProductPackings)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PackingType)
                   .WithMany(p2 => p2.Products)
                   .HasForeignKey(p => p.PackingTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductPackings");
        }
    }
}
