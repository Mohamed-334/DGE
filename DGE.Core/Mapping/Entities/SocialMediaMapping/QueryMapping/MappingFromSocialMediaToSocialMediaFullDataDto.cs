using DGE.Core.Features.SocialMedias.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.SocialMediaMapping
{
    public partial class SocialMediaMapping
    {
        #region Methods
        public void MappingFromSocialMediaToSocialMediaFullDataDto()
        {
            CreateMap<SocialMedia, SocialMediaFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<SocialMedia, SocialMediaFullDataDto>>();
        }
        #endregion
    }
}
