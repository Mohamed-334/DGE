using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.BannerMapping
{
    public partial class BannerMapping
    {
        #region Methods
        public void MappingFromBannerToDropDown()
        {
            CreateMap<Banner, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Banner, DropDown>>();
        }
        #endregion
    }
}
