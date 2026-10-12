using AutoMapper;

namespace DGE.Core.Mapping.ProductMineralMapping
{
    public partial class ProductMineralMapping : Profile
    {
        #region Constructor
        public ProductMineralMapping()
        {
            MapFromAddProductMineralCommandRequestModelToProductMineralEntity();
            MapFromUpdateProductMineralCommandRequestModelToProductMineralEntity();
            MappingFromProductMineralToProductMineralFullDataDto();
            MappingFromProductMineralToDropDown();
        }
        #endregion
    }
}
