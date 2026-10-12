using DGE.Core.Features.ProductPackings.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductPackingMapping
{
    public partial class ProductPackingMapping
    {
        #region Methods
        public void MapFromAddProductPackingCommandRequestModelToProductPackingEntity()
        {
            CreateMap<AddProductPackingCommandRequestModel, ProductPacking>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.PackingType, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductPackingCommandRequestModel, ProductPacking>>();
        }
        #endregion
    }
}
