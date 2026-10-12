using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class PackingType : BaseEntityWithName
    {

        public ICollection<ProductPacking> Products { get; set; } = new List<ProductPacking>();
    }
        }