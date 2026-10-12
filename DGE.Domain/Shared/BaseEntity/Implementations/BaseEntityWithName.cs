using DGE.Domain.Shared.BaseEntity.Interfaces;

namespace DGE.Domain.Shared.BaseEntity.Implementations
{
    public class BaseEntityWithName : BaseEntity, IBaseEntityWithName
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
    }
}
