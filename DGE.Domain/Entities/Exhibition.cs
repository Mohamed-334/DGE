using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class Exhibition : BaseEntityWithName
    {
        public string? ExhibitionUrl { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }


    }
}