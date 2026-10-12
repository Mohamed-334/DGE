using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductVarietyConfiguration : IEntityTypeConfiguration<ProductVariety>
    {
        public void Configure(EntityTypeBuilder<ProductVariety> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(p2 => p2.ProductVarieties)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductVarieties");
        }
    }
}
