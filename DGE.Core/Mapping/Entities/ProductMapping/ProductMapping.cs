using AutoMapper;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping : Profile
    {
        #region Constructor
        public ProductMapping()
        {
            MapFromAddProductCommandRequestModelToProductEntity();
            MapFromUpdateProductCommandRequestModelToProductEntity();
            MappingFromProductToProductFullDataDto();
            MappingFromProductToDropDown();
        }
        #endregion
    }
}
