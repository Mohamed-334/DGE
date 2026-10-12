using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductHarvestMonthMapping
{
    public partial class ProductHarvestMonthMapping
    {
        #region Methods
        public void MappingFromProductHarvestMonthToDropDown()
        {
            CreateMap<ProductHarvestMonth, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductHarvestMonth, DropDown>>();
        }
        #endregion
    }
}
