using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.SocialMediaMapping
{
    public partial class SocialMediaMapping
    {
        #region Methods
        public void MapFromUpdateSocialMediaCommandRequestModelToSocialMediaEntity()
        {
            CreateMap<UpdateSocialMediaCommandRequestModel, SocialMedia>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform == null ? null : src.Platform.Trim().ToLowerInvariant()))
                .ForMember(dest => dest.DisplayOrder, opt => { opt.PreCondition(src => src.DisplayOrder.HasValue); opt.MapFrom(src => src.DisplayOrder!.Value); })
                .ForMember(dest => dest.IsActive, opt => { opt.PreCondition(src => src.IsActive.HasValue); opt.MapFrom(src => src.IsActive!.Value); })
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateSocialMediaCommandRequestModel, SocialMedia>>();
        }
        #endregion
    }
}
