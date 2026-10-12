using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductVitaminConfiguration : IEntityTypeConfiguration<ProductVitamin>
    {
        public void Configure(EntityTypeBuilder<ProductVitamin> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(p2 => p2.ProductVitamins)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Vitamin)
                   .WithMany(v => v.ProductVitamins)
                   .HasForeignKey(p => p.VitaminId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductVitamins");
        }
    }
}
