using AutoMapper;
using DGE.Core.Features.Types.Dto;
using DGE.Core.Features.Types.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Types.Queries.Handler
{
    public class TypeHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetTypesListQueryRequestModel, Response<List<TypeFullDataDto>>>,
                                    IRequestHandler<GetTypeByIdQueryRequestModel, Response<TypeFullDataDto>>,
                                    IRequestHandler<GetTypesPaginatedListQueryRequestModel, Response<PaginatedList<TypeFullDataDto>>>,
                                    IRequestHandler<GetTypesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly ITypeService _typeService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _typeMapping;
        #endregion

        #region Constructor
        public TypeHandlerQuery(ITypeService typeService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper typeMapping) : base(stringLocalizer)
        {
            _typeService = typeService;
            _stringLocalizer = stringLocalizer;
            _typeMapping = typeMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<TypeFullDataDto>>> Handle(GetTypesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Types = await _typeService.GetAllAsync();
            if (Types == null)
                return NotFound<List<TypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var TypesDto = _typeMapping.Map<List<TypeFullDataDto>>(Types).ToList();
            return Success(TypesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = TypesDto.Count });
        }

        public async Task<Response<TypeFullDataDto>> Handle(GetTypeByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Type = await _typeService.GetByIdAsync(request.Id);
            if (Type == null)
                return NotFound<TypeFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var TypeDto = _typeMapping.Map<TypeFullDataDto>(Type);
            return Success(TypeDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<TypeFullDataDto>>> Handle(GetTypesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _typeService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<TypeFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var TypeFullDataDtoList = _typeMapping.Map<List<TypeFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<TypeFullDataDto>.Success(TypeFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetTypesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Types = await _typeService.GetAllAsync();
            if (Types == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _typeMapping.Map<List<DropDown>>(Types).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
