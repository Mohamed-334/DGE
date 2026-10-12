using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class Vitamin : BaseEntityWithName
    {
        public string? VitaminUrl { get; set; }
        public ICollection<ProductVitamin>? ProductVitamins { get; set; }
    }
}