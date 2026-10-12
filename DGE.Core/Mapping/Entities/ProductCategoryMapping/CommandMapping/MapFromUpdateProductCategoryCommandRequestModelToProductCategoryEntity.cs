using DGE.Core.Features.ProductCategories.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductCategoryMapping
{
    public partial class ProductCategoryMapping
    {
        #region Methods
        public void MapFromUpdateProductCategoryCommandRequestModelToProductCategoryEntity()
        {
            CreateMap<UpdateProductCategoryCommandRequestModel, ProductCategory>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateProductCategoryCommandRequestModel, ProductCategory>>();
        }
        #endregion
    }
}
