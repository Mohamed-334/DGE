using DGE.Core.Features.ProductTypes.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductTypeMapping
{
    public partial class ProductTypeMapping
    {
        #region Methods
        public void MapFromUpdateProductTypeCommandRequestModelToProductTypeEntity()
        {
            CreateMap<UpdateProductTypeCommandRequestModel, ProductType>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateProductTypeCommandRequestModel, ProductType>>();
        }
        #endregion
    }
}
