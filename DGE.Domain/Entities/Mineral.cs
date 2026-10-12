using DGE.Domain.Shared.BaseEntity.Implementations;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Domain.Entities
{
    public class Mineral : BaseEntityWithName
    {
        public string? MineralUrl { get; set; }
        public ICollection<ProductMineral>? ProductMinerals { get; set; }
    }
}