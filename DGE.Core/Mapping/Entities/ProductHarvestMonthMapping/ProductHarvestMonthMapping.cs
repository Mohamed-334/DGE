using AutoMapper;

namespace DGE.Core.Mapping.ProductHarvestMonthMapping
{
    public partial class ProductHarvestMonthMapping : Profile
    {
        #region Constructor
        public ProductHarvestMonthMapping()
        {
            MapFromAddProductHarvestMonthCommandRequestModelToProductHarvestMonthEntity();
            MapFromUpdateProductHarvestMonthCommandRequestModelToProductHarvestMonthEntity();
            MappingFromProductHarvestMonthToProductHarvestMonthFullDataDto();
            MappingFromProductHarvestMonthToDropDown();
        }
        #endregion
    }
}
