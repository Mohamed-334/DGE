using AutoMapper;

namespace DGE.Core.Mapping.TypeMapping
{
    public partial class TypeMapping : Profile
    {
        #region Constructor
        public TypeMapping()
        {
            MapFromAddTypeCommandRequestModelToTypeEntity();
            MapFromUpdateTypeCommandRequestModelToTypeEntity();
            MappingFromTypeToTypeFullDataDto();
            MappingFromTypeToDropDown();
        }
        #endregion
    }
}
