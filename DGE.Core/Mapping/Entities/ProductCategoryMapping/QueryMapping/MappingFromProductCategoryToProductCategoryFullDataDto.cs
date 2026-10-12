using DGE.Core.Features.ProductCategories.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductCategoryMapping
{
    public partial class ProductCategoryMapping
    {
        #region Methods
        public void MappingFromProductCategoryToProductCategoryFullDataDto()
        {
            CreateMap<ProductCategory, ProductCategoryFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<ProductCategory, ProductCategoryFullDataDto>>();
        }
        #endregion
    }
}
