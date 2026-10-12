using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductPackingMapping
{
    public partial class ProductPackingMapping
    {
        #region Methods
        public void MappingFromProductPackingToDropDown()
        {
            CreateMap<ProductPacking, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductPacking, DropDown>>();
        }
        #endregion
    }
}
