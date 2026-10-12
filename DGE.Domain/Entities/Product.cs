using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class Product : BaseEntityWithName
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }

        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductVariety>? ProductVarieties { get; set; }
        public ICollection<ProductType>? ProductTypes { get; set; }
        public ICollection<ProductVitamin>? ProductVitamins { get; set; }
        public ICollection<ProductMineral>? ProductMinerals { get; set; }
        public ICollection<ProductHarvestMonth>? ProductHarvestMonths { get; set; }
        public ICollection<ProductPacking>? ProductPackings { get; set; }

    }
}