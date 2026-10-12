using AutoMapper;
using DGE.Core.Features.ProductTypes.Dto;
using DGE.Core.Features.ProductTypes.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductTypes.Queries.Handler
{
    public class ProductTypeHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetProductTypesListQueryRequestModel, Response<List<ProductTypeFullDataDto>>>,
                                    IRequestHandler<GetProductTypeByIdQueryRequestModel, Response<ProductTypeFullDataDto>>,
                                    IRequestHandler<GetProductTypesPaginatedListQueryRequestModel, Response<PaginatedList<ProductTypeFullDataDto>>>,
                                    IRequestHandler<GetProductTypesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IProductTypeService _productTypeService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _productTypeMapping;
        #endregion

        #region Constructor
        public ProductTypeHandlerQuery(IProductTypeService productTypeService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper productTypeMapping) : base(stringLocalizer)
        {
            _productTypeService = productTypeService;
            _stringLocalizer = stringLocalizer;
            _productTypeMapping = productTypeMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ProductTypeFullDataDto>>> Handle(GetProductTypesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductTypes = await _productTypeService.GetAllWithIncludesAsync();
            if (ProductTypes == null)
                return NotFound<List<ProductTypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductTypesDto = _productTypeMapping.Map<List<ProductTypeFullDataDto>>(ProductTypes).ToList();
            return Success(ProductTypesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ProductTypesDto.Count });
        }

        public async Task<Response<ProductTypeFullDataDto>> Handle(GetProductTypeByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductType = await _productTypeService.GetByIdWithIncludesAsync(request.Id);
            if (ProductType == null)
                return NotFound<ProductTypeFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductTypeDto = _productTypeMapping.Map<ProductTypeFullDataDto>(ProductType);
            return Success(ProductTypeDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ProductTypeFullDataDto>>> Handle(GetProductTypesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _productTypeService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.ProductId, request.TypeId);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ProductTypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ProductTypeFullDataDtoList = _productTypeMapping.Map<List<ProductTypeFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ProductTypeFullDataDto>.Success(ProductTypeFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetProductTypesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ProductTypes = await _productTypeService.GetAllAsync();
            if (ProductTypes == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _productTypeMapping.Map<List<DropDown>>(ProductTypes).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
