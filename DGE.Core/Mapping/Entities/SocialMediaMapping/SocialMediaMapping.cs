using AutoMapper;

namespace DGE.Core.Mapping.SocialMediaMapping
{
    public partial class SocialMediaMapping : Profile
    {
        #region Constructor
        public SocialMediaMapping()
        {
            MapFromAddSocialMediaCommandRequestModelToSocialMediaEntity();
            MapFromUpdateSocialMediaCommandRequestModelToSocialMediaEntity();
            MappingFromSocialMediaToSocialMediaFullDataDto();
            MappingFromSocialMediaToDropDown();
        }
        #endregion
    }
}
