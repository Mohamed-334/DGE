using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.CertificationMapping
{
    public partial class CertificationMapping
    {
        #region Methods
        public void MappingFromCertificationToDropDown()
        {
            CreateMap<Certification, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Certification, DropDown>>();
        }
        #endregion
    }
}
