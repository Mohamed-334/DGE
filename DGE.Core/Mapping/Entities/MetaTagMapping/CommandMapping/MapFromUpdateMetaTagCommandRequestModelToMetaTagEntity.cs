using DGE.Core.Features.MetaTags.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.MetaTagMapping
{
    public partial class MetaTagMapping
    {
        #region Methods
        public void MapFromUpdateMetaTagCommandRequestModelToMetaTagEntity()
        {
            // Page and image are set by the handler
            CreateMap<UpdateMetaTagCommandRequestModel, MetaTag>()
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.OgImage, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateMetaTagCommandRequestModel, MetaTag>>();
        }
        #endregion
    }
}
