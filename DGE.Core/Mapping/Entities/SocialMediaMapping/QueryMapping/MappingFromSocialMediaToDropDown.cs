using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.SocialMediaMapping
{
    public partial class SocialMediaMapping
    {
        #region Methods
        public void MappingFromSocialMediaToDropDown()
        {
            CreateMap<SocialMedia, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<SocialMedia, DropDown>>();
        }
        #endregion
    }
}
