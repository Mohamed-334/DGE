using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductVarietyMapping
{
    public partial class ProductVarietyMapping
    {
        #region Methods
        public void MappingFromProductVarietyToDropDown()
        {
            CreateMap<ProductVariety, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductVariety, DropDown>>();
        }
        #endregion
    }
}
