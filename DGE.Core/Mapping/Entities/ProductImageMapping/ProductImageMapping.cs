using AutoMapper;

namespace DGE.Core.Mapping.ProductImageMapping
{
    public partial class ProductImageMapping : Profile
    {
        #region Constructor
        public ProductImageMapping()
        {
            MapFromAddProductImageCommandRequestModelToProductImageEntity();
            MapFromUpdateProductImageCommandRequestModelToProductImageEntity();
            MappingFromProductImageToProductImageFullDataDto();
            MappingFromProductImageToDropDown();
        }
        #endregion
    }
}
