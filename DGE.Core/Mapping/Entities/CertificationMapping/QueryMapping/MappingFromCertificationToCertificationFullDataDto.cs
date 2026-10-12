using DGE.Core.Features.Certifications.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.CertificationMapping
{
    public partial class CertificationMapping
    {
        #region Methods
        public void MappingFromCertificationToCertificationFullDataDto()
        {
            CreateMap<Certification, CertificationFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Certification, CertificationFullDataDto>>();
        }
        #endregion
    }
}
