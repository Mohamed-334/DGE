using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.BannerMapping
{
    public partial class BannerMapping
    {
        #region Methods
        public void MapFromUpdateBannerCommandRequestModelToBannerEntity()
        {
            CreateMap<UpdateBannerCommandRequestModel, Banner>()
                .ForMember(dest => dest.DisplayOrder, opt => { opt.PreCondition(src => src.DisplayOrder.HasValue); opt.MapFrom(src => src.DisplayOrder!.Value); })
                .ForMember(dest => dest.IsActive, opt => { opt.PreCondition(src => src.IsActive.HasValue); opt.MapFrom(src => src.IsActive!.Value); })
                .ForMember(dest => dest.Image, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateBannerCommandRequestModel, Banner>>();
        }
        #endregion
    }
}
