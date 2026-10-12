using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ProductImageMapping
{
    public partial class ProductImageMapping
    {
        #region Methods
        public void MappingFromProductImageToDropDown()
        {
            CreateMap<ProductImage, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductImage, DropDown>>();
        }
        #endregion
    }
}
