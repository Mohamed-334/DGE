using DGE.Core.Features.Certifications.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.CertificationMapping
{
    public partial class CertificationMapping
    {
        #region Methods
        public void MapFromAddCertificationCommandRequestModelToCertificationEntity()
        {
            CreateMap<AddCertificationCommandRequestModel, Certification>()
                .ForMember(dest => dest.CertificationUrl, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddCertificationCommandRequestModel, Certification>>();
        }
        #endregion
    }
}
