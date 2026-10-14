using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.SocialMediaMapping
{
    public partial class SocialMediaMapping
    {
        #region Methods
        public void MapFromAddSocialMediaCommandRequestModelToSocialMediaEntity()
        {
            CreateMap<AddSocialMediaCommandRequestModel, SocialMedia>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform == null ? null : src.Platform.Trim().ToLowerInvariant()))
            .AfterMap<MetaMappingDataBasedOnDestination<AddSocialMediaCommandRequestModel, SocialMedia>>();
        }
        #endregion
    }
}
