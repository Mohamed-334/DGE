using AutoMapper;
using DGE.Core.Features.ProductCategories.Dto;
using DGE.Core.Features.ProductCategories.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductCategories.Queries.Handler
{
    public class ProductCategoryHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductCategoriesListQueryRequestModel, Response<List<ProductCategoryFullDataDto>>>,
                                    IRequestHandler<GetProductCategoryByIdQueryRequestModel, Response<ProductCategoryFullDataDto>>,
                                    IRequestHandler<GetProductCategoriesPaginatedListQueryRequestModel, Response<PaginatedList<ProductCategoryFullDataDto>>>,
                                    IRequestHandler<GetProductCategoriesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductCategoryService _productCategoryService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productCategoryMapping;
        #endregion

        #region Constructor
        public ProductCategoryHandlerQuery(IProductCategoryService productCategoryService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productCategoryMapping) : base(stringLocalizer)
        {
            _productCategoryService = productCategoryService;
            _stringLocalizer = stringLocalizer;
            _productCategoryMapping = productCategoryMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductCategoryFullDataDto>>> Handle(GetProductCategoriesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategories = await _productCategoryService.GetAllAsync();
            if (ProductCategories == null)
                return NotFound<List<ProductCategoryFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductCategoriesDto = _productCategoryMapping.Map<List<ProductCategoryFullDataDto>>(ProductCategories).ToList();
            return Success(ProductCategoriesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductCategoriesDto.Count });
        }

        public async Task<Response<ProductCategoryFullDataDto>> Handle(GetProductCategoryByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategory = await _productCategoryService.GetByIdAsync(request.Id);
            if (ProductCategory == null)
                return NotFound<ProductCategoryFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductCategoryDto = _productCategoryMapping.Map<ProductCategoryFullDataDto>(ProductCategory);
            return Success(ProductCategoryDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductCategoryFullDataDto>>> Handle(GetProductCategoriesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productCategoryService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductCategoryFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductCategoryFullDataDtoList = _productCategoryMapping.Map<List<ProductCategoryFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductCategoryFullDataDto>.Success(ProductCategoryFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductCategoriesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategories = await _productCategoryService.GetAllAsync();
            if (ProductCategories == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productCategoryMapping.Map<List<DropDown>>(ProductCategories).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
