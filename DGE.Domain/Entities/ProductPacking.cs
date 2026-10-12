using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class ProductPacking : BaseEntity
    {
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product  { get; set; }
        [ForeignKey("PackingType")]
        public int? PackingTypeId { get; set; }
        public PackingType? PackingType { get; set; }     

    }
}