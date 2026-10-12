using DGE.Core.Features.ProductVarieties.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductVarietyMapping
{
    public partial class ProductVarietyMapping
    {
        #region Methods
        public void MapFromAddProductVarietyCommandRequestModelToProductVarietyEntity()
        {
            CreateMap<AddProductVarietyCommandRequestModel, ProductVariety>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductVarietyCommandRequestModel, ProductVariety>>();
        }
        #endregion
    }
}
