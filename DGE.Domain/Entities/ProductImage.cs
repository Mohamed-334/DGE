using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class ProductImage :BaseEntity
    {
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public string? ImagePath { get; set; }


    }
}
