using AutoMapper;
using DGE.Core.Features.ProductImages.Dto;
using DGE.Core.Features.ProductImages.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductImages.Queries.Handler
{
    public class ProductImageHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductImagesListQueryRequestModel, Response<List<ProductImageFullDataDto>>>,
                                    IRequestHandler<GetProductImageByIdQueryRequestModel, Response<ProductImageFullDataDto>>,
                                    IRequestHandler<GetProductImagesPaginatedListQueryRequestModel, Response<PaginatedList<ProductImageFullDataDto>>>,
                                    IRequestHandler<GetProductImagesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductImageService _productImageService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productImageMapping;
        #endregion

        #region Constructor
        public ProductImageHandlerQuery(IProductImageService productImageService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productImageMapping) : base(stringLocalizer)
        {
            _productImageService = productImageService;
            _stringLocalizer = stringLocalizer;
            _productImageMapping = productImageMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductImageFullDataDto>>> Handle(GetProductImagesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImages = await _productImageService.GetAllWithIncludesAsync();
            if (ProductImages == null)
                return NotFound<List<ProductImageFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductImagesDto = _productImageMapping.Map<List<ProductImageFullDataDto>>(ProductImages).ToList();
            return Success(ProductImagesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductImagesDto.Count });
        }

        public async Task<Response<ProductImageFullDataDto>> Handle(GetProductImageByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImage = await _productImageService.GetByIdWithIncludesAsync(request.Id);
            if (ProductImage == null)
                return NotFound<ProductImageFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductImageDto = _productImageMapping.Map<ProductImageFullDataDto>(ProductImage);
            return Success(ProductImageDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductImageFullDataDto>>> Handle(GetProductImagesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productImageService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductImageFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductImageFullDataDtoList = _productImageMapping.Map<List<ProductImageFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductImageFullDataDto>.Success(ProductImageFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductImagesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImages = await _productImageService.GetAllAsync();
            if (ProductImages == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productImageMapping.Map<List<DropDown>>(ProductImages).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
