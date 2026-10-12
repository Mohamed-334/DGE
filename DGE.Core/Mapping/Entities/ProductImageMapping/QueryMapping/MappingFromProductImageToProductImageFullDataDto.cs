using DGE.Core.Features.ProductImages.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductImageMapping
{
    public partial class ProductImageMapping
    {
        #region Methods
        public void MappingFromProductImageToProductImageFullDataDto()
        {
            CreateMap<ProductImage, ProductImageFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductImage, ProductImageFullDataDto>>();
        }
        #endregion
    }
}
