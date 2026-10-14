using DGE.Core.Features.Banners.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.BannerMapping
{
    public partial class BannerMapping
    {
        #region Methods
        public void MappingFromBannerToBannerFullDataDto()
        {
            CreateMap<Banner, BannerFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Banner, BannerFullDataDto>>();
        }
        #endregion
    }
}
