using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping
    {
        #region Methods
        public void MapFromAddProductCommandRequestModelToProductEntity()
        {
            CreateMap<AddProductCommandRequestModel, Product>()
                .ForMember(dest => dest.ProductCategory, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductCommandRequestModel, Product>>();
        }
        #endregion
    }
}
