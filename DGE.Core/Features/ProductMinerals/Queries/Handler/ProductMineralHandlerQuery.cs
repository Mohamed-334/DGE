using AutoMapper;
using DGE.Core.Features.ProductMinerals.Dto;
using DGE.Core.Features.ProductMinerals.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductMinerals.Queries.Handler
{
    public class ProductMineralHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductMineralsListQueryRequestModel, Response<List<ProductMineralFullDataDto>>>,
                                    IRequestHandler<GetProductMineralByIdQueryRequestModel, Response<ProductMineralFullDataDto>>,
                                    IRequestHandler<GetProductMineralsPaginatedListQueryRequestModel, Response<PaginatedList<ProductMineralFullDataDto>>>,
                                    IRequestHandler<GetProductMineralsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductMineralService _productMineralService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productMineralMapping;
        #endregion

        #region Constructor
        public ProductMineralHandlerQuery(IProductMineralService productMineralService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productMineralMapping) : base(stringLocalizer)
        {
            _productMineralService = productMineralService;
            _stringLocalizer = stringLocalizer;
            _productMineralMapping = productMineralMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductMineralFullDataDto>>> Handle(GetProductMineralsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMinerals = await _productMineralService.GetAllWithIncludesAsync();
            if (ProductMinerals == null)
                return NotFound<List<ProductMineralFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductMineralsDto = _productMineralMapping.Map<List<ProductMineralFullDataDto>>(ProductMinerals).ToList();
            return Success(ProductMineralsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductMineralsDto.Count });
        }

        public async Task<Response<ProductMineralFullDataDto>> Handle(GetProductMineralByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMineral = await _productMineralService.GetByIdWithIncludesAsync(request.Id);
            if (ProductMineral == null)
                return NotFound<ProductMineralFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductMineralDto = _productMineralMapping.Map<ProductMineralFullDataDto>(ProductMineral);
            return Success(ProductMineralDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductMineralFullDataDto>>> Handle(GetProductMineralsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productMineralService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId, request.MineralId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductMineralFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductMineralFullDataDtoList = _productMineralMapping.Map<List<ProductMineralFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductMineralFullDataDto>.Success(ProductMineralFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductMineralsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMinerals = await _productMineralService.GetAllAsync();
            if (ProductMinerals == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productMineralMapping.Map<List<DropDown>>(ProductMinerals).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
