using AutoMapper;

namespace DGE.Core.Mapping.MetaTagMapping
{
    public partial class MetaTagMapping : Profile
    {
        #region Constructor
        public MetaTagMapping()
        {
            MapFromUpdateMetaTagCommandRequestModelToMetaTagEntity();
            MappingFromMetaTagToMetaTagFullDataDto();
        }
        #endregion
    }
}
