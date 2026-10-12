using DGE.Core.Features.Products.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ProductMapping
{
    public partial class ProductMapping
    {
        #region Methods
        public void MappingFromProductToProductFullDataDto()
        {
            CreateMap<Product, ProductFullDataDto>()
                .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.ProductCategory != null ? src.ProductCategory.Name : null))
                .ForMember(dest => dest.Images, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductImages); })
                .ForMember(dest => dest.Varieties, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductVarieties); })
                .ForMember(dest => dest.Types, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductTypes); })
                .ForMember(dest => dest.PackingTypes, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductPackings); })
                .ForMember(dest => dest.Vitamins, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductVitamins); })
                .ForMember(dest => dest.Minerals, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductMinerals); })
                .ForMember(dest => dest.HarvestMonths, opt => { opt.AllowNull(); opt.MapFrom(src => src.ProductHarvestMonths); })
                .AfterMap<MetaMappingDataBasedOnSource<Product, ProductFullDataDto>>();

            CreateMap<ProductImage, ProductImageItemDto>();
            CreateMap<ProductVariety, ProductLookupItemDto>();
            CreateMap<ProductType, ProductLookupItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type!.Name))
                .ForMember(dest => dest.NameLocalization, opt => opt.MapFrom(src => src.Type!.NameLocalization));
            CreateMap<ProductPacking, ProductLookupItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PackingTypeId ?? 0))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PackingType!.Name))
                .ForMember(dest => dest.NameLocalization, opt => opt.MapFrom(src => src.PackingType!.NameLocalization));
            CreateMap<ProductVitamin, ProductLookupItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VitaminId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Vitamin!.Name))
                .ForMember(dest => dest.NameLocalization, opt => opt.MapFrom(src => src.Vitamin!.NameLocalization));
            CreateMap<ProductMineral, ProductLookupItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MineralId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Mineral!.Name))
                .ForMember(dest => dest.NameLocalization, opt => opt.MapFrom(src => src.Mineral!.NameLocalization));
            CreateMap<ProductHarvestMonth, ProductHarvestMonthItemDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.MonthStatusCode))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.MonthStatus))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.MonthStatusDisplayName));
        }
        #endregion
    }
}
