using AutoMapper;

namespace DGE.Core.Mapping.BannerMapping
{
    public partial class BannerMapping : Profile
    {
        #region Constructor
        public BannerMapping()
        {
            MapFromAddBannerCommandRequestModelToBannerEntity();
            MapFromUpdateBannerCommandRequestModelToBannerEntity();
            MappingFromBannerToBannerFullDataDto();
            MappingFromBannerToDropDown();
        }
        #endregion
    }
}
