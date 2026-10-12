using AutoMapper;
using DGE.Core.Features.Products.Dto;
using DGE.Core.Features.Products.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Products.Queries.Handler
{
    public class ProductHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductsListQueryRequestModel, Response<List<ProductFullDataDto>>>,
                                    IRequestHandler<GetProductByIdQueryRequestModel, Response<ProductFullDataDto>>,
                                    IRequestHandler<GetProductsPaginatedListQueryRequestModel, Response<PaginatedList<ProductFullDataDto>>>,
                                    IRequestHandler<GetProductsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productMapping;
        #endregion

        #region Constructor
        public ProductHandlerQuery(IProductService productService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productMapping) : base(stringLocalizer)
        {
            _productService = productService;
            _stringLocalizer = stringLocalizer;
            _productMapping = productMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductFullDataDto>>> Handle(GetProductsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Products = await _productService.GetAllWithIncludesAsync();
            if (Products == null)
                return NotFound<List<ProductFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductsDto = _productMapping.Map<List<ProductFullDataDto>>(Products).ToList();
            return Success(ProductsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductsDto.Count });
        }

        public async Task<Response<ProductFullDataDto>> Handle(GetProductByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Product = await _productService.GetByIdWithIncludesAsync(request.Id);
            if (Product == null)
                return NotFound<ProductFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductDto = _productMapping.Map<ProductFullDataDto>(Product);
            return Success(ProductDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductFullDataDto>>> Handle(GetProductsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search, request.ProductCategoryId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductFullDataDtoList = _productMapping.Map<List<ProductFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductFullDataDto>.Success(ProductFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Products = await _productService.GetAllAsync();
            if (Products == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productMapping.Map<List<DropDown>>(Products).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
