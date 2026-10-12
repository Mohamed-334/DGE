using DGE.Core.Features.ProductCategories.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductCategoryMapping
{
    public partial class ProductCategoryMapping
    {
        #region Methods
        public void MapFromAddProductCategoryCommandRequestModelToProductCategoryEntity()
        {
            CreateMap<AddProductCategoryCommandRequestModel, ProductCategory>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductCategoryCommandRequestModel, ProductCategory>>();
        }
        #endregion
    }
}
