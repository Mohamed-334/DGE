using AutoMapper;
using DGE.Core.Features.Permissions.Dto;
using DGE.Core.Features.Permissions.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Permissions.Queries.Handler
{
    public class PermissionHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetPermissionsListQueryRequestModel, Response<List<PermissionFullDataDto>>>,
                                    IRequestHandler<GetPermissionByIdQueryRequestModel, Response<PermissionFullDataDto>>,
                                    IRequestHandler<GetPermissionsPaginatedListQueryRequestModel, Response<PaginatedList<PermissionFullDataDto>>>,
                                    IRequestHandler<GetPermissionsDropDownQueryRequestModel, Response<List<DropDown>>>
    {

        #region Fields
        private readonly IPermissionService _permissionService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _permissionMapping;
        #endregion

        #region Constructor
        public PermissionHandlerQuery(IPermissionService permissionService, IStringLocalizer<AppLocalization> stringLocalizer, IHttpContextAccessor httpContextAccessor, IMapper permissionMapping) : base(stringLocalizer)
        {
            _permissionService = permissionService;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _permissionMapping = permissionMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<PermissionFullDataDto>>> Handle(GetPermissionsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Permissions = await _permissionService.GetAllAsync(cancellationToken);
            if (Permissions == null)
                return NotFound<List<PermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PermissionsDto = _permissionMapping.Map<List<PermissionFullDataDto>>(Permissions).ToList();
            return Success(PermissionsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = PermissionsDto.Count });
        }

        public async Task<Response<PermissionFullDataDto>> Handle(GetPermissionByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Permission = await _permissionService.GetByIdNoTrackingAsync(request.Id, cancellationToken);
            if (Permission == null)
                return NotFound<PermissionFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PermissionDto = _permissionMapping.Map<PermissionFullDataDto>(Permission);
            return Success(PermissionDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<PermissionFullDataDto>>> Handle(GetPermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _permissionService.GetPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (PaginatedList == null)
                return NotFound<PaginatedList<PermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var PermissionFullDataDtoList = _permissionMapping.Map<List<PermissionFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<PermissionFullDataDto>.Success(PermissionFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        public async Task<Response<List<DropDown>>> Handle(GetPermissionsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Permissions = await _permissionService.GetAllAsync(cancellationToken);
            if (Permissions == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _permissionMapping.Map<List<DropDown>>(Permissions).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion

    }
}
