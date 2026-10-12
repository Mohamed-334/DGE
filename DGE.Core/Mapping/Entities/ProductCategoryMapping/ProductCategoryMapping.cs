using AutoMapper;

namespace DGE.Core.Mapping.ProductCategoryMapping
{
    public partial class ProductCategoryMapping : Profile
    {
        #region Constructor
        public ProductCategoryMapping()
        {
            MapFromAddProductCategoryCommandRequestModelToProductCategoryEntity();
            MapFromUpdateProductCategoryCommandRequestModelToProductCategoryEntity();
            MappingFromProductCategoryToProductCategoryFullDataDto();
            MappingFromProductCategoryToDropDown();
        }
        #endregion
    }
}
