using DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Enums;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Mapping.ProductHarvestMonthMapping
{
    public partial class ProductHarvestMonthMapping
    {
        #region Methods
        public void MapFromUpdateProductHarvestMonthCommandRequestModelToProductHarvestMonthEntity()
        {
            CreateMap<UpdateProductHarvestMonthCommandRequestModel, ProductHarvestMonth>()
                .ForMember(dest => dest.MonthStatus, opt => opt.MapFrom(src => src.MonthStatusCode.HasValue ? ((Month)src.MonthStatusCode).SplitEnumName() : null))
                .ForMember(dest => dest.MonthStatusDisplayName, opt => opt.MapFrom(src => src.MonthStatusCode.HasValue ? ((Month)src.MonthStatusCode).GetDisplayName() : null))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateProductHarvestMonthCommandRequestModel, ProductHarvestMonth>>();
        }
        #endregion
    }
}
