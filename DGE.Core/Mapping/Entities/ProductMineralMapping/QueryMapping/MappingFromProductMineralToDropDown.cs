using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductMineralMapping
{
    public partial class ProductMineralMapping
    {
        #region Methods
        public void MappingFromProductMineralToDropDown()
        {
            CreateMap<ProductMineral, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductMineral, DropDown>>();
        }
        #endregion
    }
}
