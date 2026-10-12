using AutoMapper;
using DGE.Core.Features.ProductVitamins.Dto;
using DGE.Core.Features.ProductVitamins.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductVitamins.Queries.Handler
{
    public class ProductVitaminHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductVitaminsListQueryRequestModel, Response<List<ProductVitaminFullDataDto>>>,
                                    IRequestHandler<GetProductVitaminByIdQueryRequestModel, Response<ProductVitaminFullDataDto>>,
                                    IRequestHandler<GetProductVitaminsPaginatedListQueryRequestModel, Response<PaginatedList<ProductVitaminFullDataDto>>>,
                                    IRequestHandler<GetProductVitaminsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductVitaminService _productVitaminService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productVitaminMapping;
        #endregion

        #region Constructor
        public ProductVitaminHandlerQuery(IProductVitaminService productVitaminService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productVitaminMapping) : base(stringLocalizer)
        {
            _productVitaminService = productVitaminService;
            _stringLocalizer = stringLocalizer;
            _productVitaminMapping = productVitaminMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductVitaminFullDataDto>>> Handle(GetProductVitaminsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamins = await _productVitaminService.GetAllWithIncludesAsync();
            if (ProductVitamins == null)
                return NotFound<List<ProductVitaminFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVitaminsDto = _productVitaminMapping.Map<List<ProductVitaminFullDataDto>>(ProductVitamins).ToList();
            return Success(ProductVitaminsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductVitaminsDto.Count });
        }

        public async Task<Response<ProductVitaminFullDataDto>> Handle(GetProductVitaminByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamin = await _productVitaminService.GetByIdWithIncludesAsync(request.Id);
            if (ProductVitamin == null)
                return NotFound<ProductVitaminFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVitaminDto = _productVitaminMapping.Map<ProductVitaminFullDataDto>(ProductVitamin);
            return Success(ProductVitaminDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductVitaminFullDataDto>>> Handle(GetProductVitaminsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productVitaminService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId, request.VitaminId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductVitaminFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductVitaminFullDataDtoList = _productVitaminMapping.Map<List<ProductVitaminFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductVitaminFullDataDto>.Success(ProductVitaminFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductVitaminsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamins = await _productVitaminService.GetAllAsync();
            if (ProductVitamins == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productVitaminMapping.Map<List<DropDown>>(ProductVitamins).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
