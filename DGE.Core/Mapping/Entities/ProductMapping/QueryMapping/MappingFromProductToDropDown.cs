using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping
    {
        #region Methods
        public void MappingFromProductToDropDown()
        {
            CreateMap<Product, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Product, DropDown>>();
        }
        #endregion
    }
}
