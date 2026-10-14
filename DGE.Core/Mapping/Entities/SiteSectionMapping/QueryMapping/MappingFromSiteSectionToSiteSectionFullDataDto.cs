using DGE.Core.Features.SiteSections.Dto;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.SiteSectionMapping
{
    public partial class SiteSectionMapping
    {
        #region Methods
        public void MappingFromSiteSectionToSiteSectionFullDataDto()
        {
            CreateMap<SiteSection, SiteSectionFullDataDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => (src.Items ?? new List<SiteSectionItem>()).OrderBy(i => i.DisplayOrder)));
            CreateMap<SiteSectionItem, SiteSectionItemDto>();
        }
        #endregion
    }
}
