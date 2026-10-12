using DGE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGE.Infrastructure.Context.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductCategory)
                   .WithMany(p2 => p2.Products)
                   .HasForeignKey(p => p.ProductCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Products");
        }
    }
}
