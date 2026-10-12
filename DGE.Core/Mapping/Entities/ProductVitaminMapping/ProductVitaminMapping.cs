using AutoMapper;

namespace DGE.Core.Mapping.ProductVitaminMapping
{
    public partial class ProductVitaminMapping : Profile
    {
        #region Constructor
        public ProductVitaminMapping()
        {
            MapFromAddProductVitaminCommandRequestModelToProductVitaminEntity();
            MapFromUpdateProductVitaminCommandRequestModelToProductVitaminEntity();
            MappingFromProductVitaminToProductVitaminFullDataDto();
            MappingFromProductVitaminToDropDown();
        }
        #endregion
    }
}
