using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductTypeMapping
{
    public partial class ProductTypeMapping
    {
        #region Methods
        public void MappingFromProductTypeToDropDown()
        {
            CreateMap<ProductType, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductType, DropDown>>();
        }
        #endregion
    }
}
