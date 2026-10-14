using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping
    {
        #region Methods
        public void MapFromUpdateProductCommandRequestModelToProductEntity()
        {
            CreateMap<UpdateProductCommandRequestModel, Product>()
                .ForMember(dest => dest.ProductCategory, opt => opt.Ignore())
                .ForMember(dest => dest.ShowOnHome, opt => { opt.PreCondition(src => src.ShowOnHome.HasValue); opt.MapFrom(src => src.ShowOnHome!.Value); })
                .ForMember(dest => dest.DisplayOrder, opt => { opt.PreCondition(src => src.DisplayOrder.HasValue); opt.MapFrom(src => src.DisplayOrder!.Value); })
                // The handler decides the slug (keeps the current one when none is sent)
                .ForMember(dest => dest.Slug, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateProductCommandRequestModel, Product>>();
        }
        #endregion
    }
}
