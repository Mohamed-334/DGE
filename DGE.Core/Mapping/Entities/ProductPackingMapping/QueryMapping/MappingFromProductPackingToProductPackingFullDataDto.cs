using DGE.Core.Features.ProductPackings.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductPackingMapping
{
    public partial class ProductPackingMapping
    {
        #region Methods
        public void MappingFromProductPackingToProductPackingFullDataDto()
        {
            CreateMap<ProductPacking, ProductPackingFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.PackingTypeName, opt => opt.MapFrom(src => src.PackingType != null ? src.PackingType.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductPacking, ProductPackingFullDataDto>>();
        }
        #endregion
    }
}
