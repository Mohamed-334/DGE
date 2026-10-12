using AutoMapper;
using DGE.Core.Features.RolePermissions.Dto;
using DGE.Core.Features.RolePermissions.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.RolePermissions.Queries.Handler
{
    public class RolePermissionHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetRolePermissionsListQueryRequestModel, Response<List<RolePermissionFullDataDto>>>,
                                    IRequestHandler<GetRolePermissionByIdQueryRequestModel, Response<RolePermissionFullDataDto>>,
                                    IRequestHandler<GetRolePermissionsPaginatedListQueryRequestModel, Response<PaginatedList<RolePermissionFullDataDto>>>,
                                    IRequestHandler<GetRolePermissionsDropDownQueryRequestModel, Response<List<DropDown>>>
    {

        #region Fields
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _rolePermissionMapping;
        #endregion

        #region Constructor
        public RolePermissionHandlerQuery(IRolePermissionService rolePermissionService, IStringLocalizer<AppLocalization> stringLocalizer, IHttpContextAccessor httpContextAccessor, IMapper rolePermissionMapping) : base(stringLocalizer)
        {
            _rolePermissionService = rolePermissionService;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _rolePermissionMapping = rolePermissionMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<RolePermissionFullDataDto>>> Handle(GetRolePermissionsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermissions = await _rolePermissionService.GetAllAsync(cancellationToken);
            if (RolePermissions == null)
                return NotFound<List<RolePermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var RolePermissionsDto = _rolePermissionMapping.Map<List<RolePermissionFullDataDto>>(RolePermissions).ToList();
            return Success(RolePermissionsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = RolePermissionsDto.Count });
        }

        public async Task<Response<RolePermissionFullDataDto>> Handle(GetRolePermissionByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermission = await _rolePermissionService.GetByIdNoTrackingAsync(request.Id, cancellationToken);
            if (RolePermission == null)
                return NotFound<RolePermissionFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var RolePermissionDto = _rolePermissionMapping.Map<RolePermissionFullDataDto>(RolePermission);
            return Success(RolePermissionDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<RolePermissionFullDataDto>>> Handle(GetRolePermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _rolePermissionService.GetPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (PaginatedList == null)
                return NotFound<PaginatedList<RolePermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var RolePermissionFullDataDtoList = _rolePermissionMapping.Map<List<RolePermissionFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<RolePermissionFullDataDto>.Success(RolePermissionFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        public async Task<Response<List<DropDown>>> Handle(GetRolePermissionsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermissions = await _rolePermissionService.GetAllAsync(cancellationToken);
            if (RolePermissions == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _rolePermissionMapping.Map<List<DropDown>>(RolePermissions).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion

    }
}
