using AutoMapper;

namespace DGE.Core.Mapping.SiteSectionMapping
{
    public partial class SiteSectionMapping : Profile
    {
        #region Constructor
        public SiteSectionMapping()
        {
            MapFromUpdateSiteSectionCommandRequestModelToSiteSectionEntity();
            MappingFromSiteSectionToSiteSectionFullDataDto();
        }
        #endregion
    }
}
