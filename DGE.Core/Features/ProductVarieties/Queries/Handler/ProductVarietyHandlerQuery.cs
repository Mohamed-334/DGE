using AutoMapper;
using DGE.Core.Features.ProductVarieties.Dto;
using DGE.Core.Features.ProductVarieties.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductVarieties.Queries.Handler
{
    public class ProductVarietyHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductVarietiesListQueryRequestModel, Response<List<ProductVarietyFullDataDto>>>,
                                    IRequestHandler<GetProductVarietyByIdQueryRequestModel, Response<ProductVarietyFullDataDto>>,
                                    IRequestHandler<GetProductVarietiesPaginatedListQueryRequestModel, Response<PaginatedList<ProductVarietyFullDataDto>>>,
                                    IRequestHandler<GetProductVarietiesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductVarietyService _productVarietyService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productVarietyMapping;
        #endregion

        #region Constructor
        public ProductVarietyHandlerQuery(IProductVarietyService productVarietyService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productVarietyMapping) : base(stringLocalizer)
        {
            _productVarietyService = productVarietyService;
            _stringLocalizer = stringLocalizer;
            _productVarietyMapping = productVarietyMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductVarietyFullDataDto>>> Handle(GetProductVarietiesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVarieties = await _productVarietyService.GetAllWithIncludesAsync();
            if (ProductVarieties == null)
                return NotFound<List<ProductVarietyFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVarietiesDto = _productVarietyMapping.Map<List<ProductVarietyFullDataDto>>(ProductVarieties).ToList();
            return Success(ProductVarietiesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductVarietiesDto.Count });
        }

        public async Task<Response<ProductVarietyFullDataDto>> Handle(GetProductVarietyByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVariety = await _productVarietyService.GetByIdWithIncludesAsync(request.Id);
            if (ProductVariety == null)
                return NotFound<ProductVarietyFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVarietyDto = _productVarietyMapping.Map<ProductVarietyFullDataDto>(ProductVariety);
            return Success(ProductVarietyDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductVarietyFullDataDto>>> Handle(GetProductVarietiesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productVarietyService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search, request.ProductId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductVarietyFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVarietyFullDataDtoList = _productVarietyMapping.Map<List<ProductVarietyFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductVarietyFullDataDto>.Success(ProductVarietyFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductVarietiesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVarieties = await _productVarietyService.GetAllAsync();
            if (ProductVarieties == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productVarietyMapping.Map<List<DropDown>>(ProductVarieties).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
