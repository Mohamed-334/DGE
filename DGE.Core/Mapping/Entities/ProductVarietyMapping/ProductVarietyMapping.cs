using AutoMapper;

namespace DGE.Core.Mapping.ProductVarietyMapping
{
    public partial class ProductVarietyMapping : Profile
    {
        #region Constructor
        public ProductVarietyMapping()
        {
            MapFromAddProductVarietyCommandRequestModelToProductVarietyEntity();
            MapFromUpdateProductVarietyCommandRequestModelToProductVarietyEntity();
            MappingFromProductVarietyToProductVarietyFullDataDto();
            MappingFromProductVarietyToDropDown();
        }
        #endregion
    }
}
