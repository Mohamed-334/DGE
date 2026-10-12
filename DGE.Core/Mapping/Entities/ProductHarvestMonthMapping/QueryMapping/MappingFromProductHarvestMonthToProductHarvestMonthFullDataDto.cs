using DGE.Core.Features.ProductHarvestMonths.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductHarvestMonthMapping
{
    public partial class ProductHarvestMonthMapping
    {
        #region Methods
        public void MappingFromProductHarvestMonthToProductHarvestMonthFullDataDto()
        {
            CreateMap<ProductHarvestMonth, ProductHarvestMonthFullDataDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .AfterMap<MetaMappingDataBasedOnSource<ProductHarvestMonth, ProductHarvestMonthFullDataDto>>();
        }
        #endregion
    }
}
