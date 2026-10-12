using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class ProductHarvestMonth: BaseEntity
    {
        public int? MonthStatusCode { get; set; }
        public string? MonthStatus { get; set; }
        public string? MonthStatusDisplayName { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; } 
        public Product Product { get; set; } = null!;
    }
}