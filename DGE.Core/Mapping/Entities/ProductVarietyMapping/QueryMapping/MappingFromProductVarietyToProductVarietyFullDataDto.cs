using DGE.Core.Features.ProductVarieties.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductVarietyMapping
{
    public partial class ProductVarietyMapping
    {
        #region Methods
        public void MappingFromProductVarietyToProductVarietyFullDataDto()
        {
            CreateMap<ProductVariety, ProductVarietyFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductVariety, ProductVarietyFullDataDto>>();
        }
        #endregion
    }
}
