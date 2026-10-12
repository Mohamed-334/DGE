using DGE.Core.Features.ProductImages.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductImageMapping
{
    public partial class ProductImageMapping
    {
        #region Methods
        public void MapFromAddProductImageCommandRequestModelToProductImageEntity()
        {
            CreateMap<AddProductImageCommandRequestModel, ProductImage>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductImageCommandRequestModel, ProductImage>>();
        }
        #endregion
    }
}
