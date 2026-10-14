using DGE.Core.Features.ProductVitamins.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductVitaminMapping
{
    public partial class ProductVitaminMapping
    {
        #region Methods
        public void MappingFromProductVitaminToProductVitaminFullDataDto()
        {
            CreateMap<ProductVitamin, ProductVitaminFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.VitaminName, opt => opt.MapFrom(src => src.Vitamin != null ? src.Vitamin.Name : null))
                .ForMember(dest => dest.VitaminNameLocalization, opt => opt.MapFrom(src => src.Vitamin != null ? src.Vitamin.NameLocalization : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductVitamin, ProductVitaminFullDataDto>>();
        }
        #endregion
    }
}
