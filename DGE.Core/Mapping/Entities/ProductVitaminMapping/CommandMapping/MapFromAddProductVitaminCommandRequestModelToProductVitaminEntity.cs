using DGE.Core.Features.ProductVitamins.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductVitaminMapping
{
    public partial class ProductVitaminMapping
    {
        #region Methods
        public void MapFromAddProductVitaminCommandRequestModelToProductVitaminEntity()
        {
            CreateMap<AddProductVitaminCommandRequestModel, ProductVitamin>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Vitamin, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductVitaminCommandRequestModel, ProductVitamin>>();
        }
        #endregion
    }
}
