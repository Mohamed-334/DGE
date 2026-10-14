using DGE.Core.Features.MetaTags.Dto;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.MetaTagMapping
{
    public partial class MetaTagMapping
    {
        #region Methods
        public void MappingFromMetaTagToMetaTagFullDataDto()
        {
            CreateMap<MetaTag, MetaTagFullDataDto>();
        }
        #endregion
    }
}
