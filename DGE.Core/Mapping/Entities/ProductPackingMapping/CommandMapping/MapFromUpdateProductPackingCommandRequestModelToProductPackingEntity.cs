using DGE.Core.Features.ProductPackings.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductPackingMapping
{
    public partial class ProductPackingMapping
    {
        #region Methods
        public void MapFromUpdateProductPackingCommandRequestModelToProductPackingEntity()
        {
            CreateMap<UpdateProductPackingCommandRequestModel, ProductPacking>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.PackingType, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateProductPackingCommandRequestModel, ProductPacking>>();
        }
        #endregion
    }
}
