using AutoMapper;

namespace DGE.Core.Mapping.ProductTypeMapping
{
    public partial class ProductTypeMapping : Profile
    {
        #region Constructor
        public ProductTypeMapping()
        {
            MapFromAddProductTypeCommandRequestModelToProductTypeEntity();
            MapFromUpdateProductTypeCommandRequestModelToProductTypeEntity();
            MappingFromProductTypeToProductTypeFullDataDto();
            MappingFromProductTypeToDropDown();
        }
        #endregion
    }
}
