using AutoMapper;
using DGE.Core.Features.PackingTypes.Dto;
using DGE.Core.Features.PackingTypes.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.PackingTypes.Queries.Handler
{
    public class PackingTypeHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetPackingTypesListQueryRequestModel, Response<List<PackingTypeFullDataDto>>>,
                                    IRequestHandler<GetPackingTypeByIdQueryRequestModel, Response<PackingTypeFullDataDto>>,
                                    IRequestHandler<GetPackingTypesPaginatedListQueryRequestModel, Response<PaginatedList<PackingTypeFullDataDto>>>,
                                    IRequestHandler<GetPackingTypesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IPackingTypeService _packingTypeService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _packingTypeMapping;
        #endregion

        #region Constructor
        public PackingTypeHandlerQuery(IPackingTypeService packingTypeService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper packingTypeMapping) : base(stringLocalizer)
        {
            _packingTypeService = packingTypeService;
            _stringLocalizer = stringLocalizer;
            _packingTypeMapping = packingTypeMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<PackingTypeFullDataDto>>> Handle(GetPackingTypesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PackingTypes = await _packingTypeService.GetAllAsync();
            if (PackingTypes == null)
                return NotFound<List<PackingTypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PackingTypesDto = _packingTypeMapping.Map<List<PackingTypeFullDataDto>>(PackingTypes).ToList();
            return Success(PackingTypesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = PackingTypesDto.Count });
        }

        public async Task<Response<PackingTypeFullDataDto>> Handle(GetPackingTypeByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PackingType = await _packingTypeService.GetByIdAsync(request.Id);
            if (PackingType == null)
                return NotFound<PackingTypeFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PackingTypeDto = _packingTypeMapping.Map<PackingTypeFullDataDto>(PackingType);
            return Success(PackingTypeDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<PackingTypeFullDataDto>>> Handle(GetPackingTypesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _packingTypeService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<PackingTypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PackingTypeFullDataDtoList = _packingTypeMapping.Map<List<PackingTypeFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<PackingTypeFullDataDto>.Success(PackingTypeFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetPackingTypesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PackingTypes = await _packingTypeService.GetAllAsync();
            if (PackingTypes == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _packingTypeMapping.Map<List<DropDown>>(PackingTypes).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
