using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductVitaminMapping
{
    public partial class ProductVitaminMapping
    {
        #region Methods
        public void MappingFromProductVitaminToDropDown()
        {
            CreateMap<ProductVitamin, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductVitamin, DropDown>>();
        }
        #endregion
    }
}
