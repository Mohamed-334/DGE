using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class ProductCategory : BaseEntityWithName
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}