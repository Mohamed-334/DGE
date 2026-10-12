using DGE.Core.Features.Products.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping
    {
        #region Methods
        public void MappingFromProductToProductFullDataDto()
        {
            CreateMap<Product, ProductFullDataDto>()
                .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.ProductCategory != null ? src.ProductCategory.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<Product, ProductFullDataDto>>();
        }
        #endregion
    }
}
