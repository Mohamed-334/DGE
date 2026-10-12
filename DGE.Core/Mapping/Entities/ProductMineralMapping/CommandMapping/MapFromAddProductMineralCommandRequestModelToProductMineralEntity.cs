using DGE.Core.Features.ProductMinerals.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMineralMapping
{
    public partial class ProductMineralMapping
    {
        #region Methods
        public void MapFromAddProductMineralCommandRequestModelToProductMineralEntity()
        {
            CreateMap<AddProductMineralCommandRequestModel, ProductMineral>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Mineral, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddProductMineralCommandRequestModel, ProductMineral>>();
        }
        #endregion
    }
}
