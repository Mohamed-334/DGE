using AutoMapper;
using DGE.Core.Features.ProductPackings.Dto;
using DGE.Core.Features.ProductPackings.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductPackings.Queries.Handler
{
    public class ProductPackingHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductPackingsListQueryRequestModel, Response<List<ProductPackingFullDataDto>>>,
                                    IRequestHandler<GetProductPackingByIdQueryRequestModel, Response<ProductPackingFullDataDto>>,
                                    IRequestHandler<GetProductPackingsPaginatedListQueryRequestModel, Response<PaginatedList<ProductPackingFullDataDto>>>,
                                    IRequestHandler<GetProductPackingsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductPackingService _productPackingService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productPackingMapping;
        #endregion

        #region Constructor
        public ProductPackingHandlerQuery(IProductPackingService productPackingService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productPackingMapping) : base(stringLocalizer)
        {
            _productPackingService = productPackingService;
            _stringLocalizer = stringLocalizer;
            _productPackingMapping = productPackingMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductPackingFullDataDto>>> Handle(GetProductPackingsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPackings = await _productPackingService.GetAllWithIncludesAsync();
            if (ProductPackings == null)
                return NotFound<List<ProductPackingFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductPackingsDto = _productPackingMapping.Map<List<ProductPackingFullDataDto>>(ProductPackings).ToList();
            return Success(ProductPackingsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductPackingsDto.Count });
        }

        public async Task<Response<ProductPackingFullDataDto>> Handle(GetProductPackingByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPacking = await _productPackingService.GetByIdWithIncludesAsync(request.Id);
            if (ProductPacking == null)
                return NotFound<ProductPackingFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductPackingDto = _productPackingMapping.Map<ProductPackingFullDataDto>(ProductPacking);
            return Success(ProductPackingDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductPackingFullDataDto>>> Handle(GetProductPackingsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productPackingService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId, request.PackingTypeId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductPackingFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductPackingFullDataDtoList = _productPackingMapping.Map<List<ProductPackingFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductPackingFullDataDto>.Success(ProductPackingFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductPackingsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPackings = await _productPackingService.GetAllAsync();
            if (ProductPackings == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productPackingMapping.Map<List<DropDown>>(ProductPackings).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
