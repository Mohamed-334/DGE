using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class ProductVitamin :BaseEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Vitamin")]
        public int VitaminId { get; set; }
        public Vitamin? Vitamin { get; set; }
    }
}