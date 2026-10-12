using AutoMapper;

namespace DGE.Core.Mapping.PackingTypeMapping
{
    public partial class PackingTypeMapping : Profile
    {
        #region Constructor
        public PackingTypeMapping()
        {
            MapFromAddPackingTypeCommandRequestModelToPackingTypeEntity();
            MapFromUpdatePackingTypeCommandRequestModelToPackingTypeEntity();
            MappingFromPackingTypeToPackingTypeFullDataDto();
            MappingFromPackingTypeToDropDown();
        }
        #endregion
    }
}
