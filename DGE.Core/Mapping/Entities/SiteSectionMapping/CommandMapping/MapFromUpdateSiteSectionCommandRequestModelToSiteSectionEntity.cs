using DGE.Core.Features.SiteSections.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.SiteSectionMapping
{
    public partial class SiteSectionMapping
    {
        #region Methods
        public void MapFromUpdateSiteSectionCommandRequestModelToSiteSectionEntity()
        {
            // Key, image and items are set by the handler
            CreateMap<UpdateSiteSectionCommandRequestModel, SiteSection>()
                .ForMember(dest => dest.Key, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateSiteSectionCommandRequestModel, SiteSection>>();
        }
        #endregion
    }
}
