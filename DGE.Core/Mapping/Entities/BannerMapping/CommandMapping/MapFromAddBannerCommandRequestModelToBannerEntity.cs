using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.BannerMapping
{
    public partial class BannerMapping
    {
        #region Methods
        public void MapFromAddBannerCommandRequestModelToBannerEntity()
        {
            CreateMap<AddBannerCommandRequestModel, Banner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddBannerCommandRequestModel, Banner>>();
        }
        #endregion
    }
}
