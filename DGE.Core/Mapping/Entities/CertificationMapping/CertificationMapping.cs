using AutoMapper;

namespace DGE.Core.Mapping.CertificationMapping
{
    public partial class CertificationMapping : Profile
    {
        #region Constructor
        public CertificationMapping()
        {
            MapFromAddCertificationCommandRequestModelToCertificationEntity();
            MapFromUpdateCertificationCommandRequestModelToCertificationEntity();
            MappingFromCertificationToCertificationFullDataDto();
            MappingFromCertificationToDropDown();
        }
        #endregion
    }
}
