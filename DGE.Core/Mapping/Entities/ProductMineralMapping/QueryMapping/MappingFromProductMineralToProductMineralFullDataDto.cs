using DGE.Core.Features.ProductMinerals.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMineralMapping
{
    public partial class ProductMineralMapping
    {
        #region Methods
        public void MappingFromProductMineralToProductMineralFullDataDto()
        {
            CreateMap<ProductMineral, ProductMineralFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.MineralName, opt => opt.MapFrom(src => src.Mineral != null ? src.Mineral.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductMineral, ProductMineralFullDataDto>>();
        }
        #endregion
    }
}
