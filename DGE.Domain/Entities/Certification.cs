using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class Certification : BaseEntityWithName
    {
        public string? CertificationUrl { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }


    }
}