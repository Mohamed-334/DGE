using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductCategoryMapping
{
    public partial class ProductCategoryMapping
    {
        #region Methods
        public void MappingFromProductCategoryToDropDown()
        {
            CreateMap<ProductCategory, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductCategory, DropDown>>();
        }
        #endregion
    }
}
