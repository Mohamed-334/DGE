using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class Type : BaseEntityWithName
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public ICollection<ProductType>? Products { get; set; }
    }
}