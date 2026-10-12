using AutoMapper;

namespace DGE.Core.Mapping.ProductPackingMapping
{
    public partial class ProductPackingMapping : Profile
    {
        #region Constructor
        public ProductPackingMapping()
        {
            MapFromAddProductPackingCommandRequestModelToProductPackingEntity();
            MapFromUpdateProductPackingCommandRequestModelToProductPackingEntity();
            MappingFromProductPackingToProductPackingFullDataDto();
            MappingFromProductPackingToDropDown();
        }
        #endregion
    }
}
