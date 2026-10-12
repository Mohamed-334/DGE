using DGE.Core.Features.ProductTypes.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductTypeMapping
{
    public partial class ProductTypeMapping
    {
        #region Methods
        public void MappingFromProductTypeToProductTypeFullDataDto()
        {
            CreateMap<ProductType, ProductTypeFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type != null ? src.Type.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductType, ProductTypeFullDataDto>>();
        }
        #endregion
    }
}
