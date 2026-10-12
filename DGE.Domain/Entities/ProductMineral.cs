using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class ProductMineral : BaseEntity
    {

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Mineral")]
        public int MineralId { get; set; }
        public Mineral? Mineral { get; set; }
    }
}