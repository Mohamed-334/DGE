using AutoMapper;
using DGE.Core.Features.ProductHarvestMonths.Dto;
using DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductHarvestMonths.Queries.Handler
{
    public class ProductHarvestMonthHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductHarvestMonthsListQueryRequestModel, Response<List<ProductHarvestMonthFullDataDto>>>,
                                    IRequestHandler<GetProductHarvestMonthByIdQueryRequestModel, Response<ProductHarvestMonthFullDataDto>>,
                                    IRequestHandler<GetProductHarvestMonthsPaginatedListQueryRequestModel, Response<PaginatedList<ProductHarvestMonthFullDataDto>>>,
                                    IRequestHandler<GetProductHarvestMonthsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductHarvestMonthService _productHarvestMonthService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productHarvestMonthMapping;
        #endregion

        #region Constructor
        public ProductHarvestMonthHandlerQuery(IProductHarvestMonthService productHarvestMonthService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productHarvestMonthMapping) : base(stringLocalizer)
        {
            _productHarvestMonthService = productHarvestMonthService;
            _stringLocalizer = stringLocalizer;
            _productHarvestMonthMapping = productHarvestMonthMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductHarvestMonthFullDataDto>>> Handle(GetProductHarvestMonthsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonths = await _productHarvestMonthService.GetAllWithIncludesAsync();
            if (ProductHarvestMonths == null)
                return NotFound<List<ProductHarvestMonthFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductHarvestMonthsDto = _productHarvestMonthMapping.Map<List<ProductHarvestMonthFullDataDto>>(ProductHarvestMonths).ToList();
            return Success(ProductHarvestMonthsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductHarvestMonthsDto.Count });
        }

        public async Task<Response<ProductHarvestMonthFullDataDto>> Handle(GetProductHarvestMonthByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonth = await _productHarvestMonthService.GetByIdWithIncludesAsync(request.Id);
            if (ProductHarvestMonth == null)
                return NotFound<ProductHarvestMonthFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductHarvestMonthDto = _productHarvestMonthMapping.Map<ProductHarvestMonthFullDataDto>(ProductHarvestMonth);
            return Success(ProductHarvestMonthDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductHarvestMonthFullDataDto>>> Handle(GetProductHarvestMonthsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productHarvestMonthService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId, request.MonthStatusCode);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductHarvestMonthFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductHarvestMonthFullDataDtoList = _productHarvestMonthMapping.Map<List<ProductHarvestMonthFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductHarvestMonthFullDataDto>.Success(ProductHarvestMonthFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductHarvestMonthsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonths = await _productHarvestMonthService.GetAllAsync();
            if (ProductHarvestMonths == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productHarvestMonthMapping.Map<List<DropDown>>(ProductHarvestMonths).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
