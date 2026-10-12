using AutoMapper;
using DGE.Core.Features.UserPermissions.Dto;
using DGE.Core.Features.UserPermissions.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.UserPermissions.Queries.Handler
{
    public class UserPermissionHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetUserPermissionsListQueryRequestModel, Response<List<UserPermissionFullDataDto>>>,
                                    IRequestHandler<GetUserPermissionByIdQueryRequestModel, Response<UserPermissionFullDataDto>>,
                                    IRequestHandler<GetUserPermissionsPaginatedListQueryRequestModel, Response<PaginatedList<UserPermissionFullDataDto>>>,
                                    IRequestHandler<GetUserPermissionsDropDownQueryRequestModel, Response<List<DropDown>>>
    {

        #region Fields
        private readonly IUserPermissionService _userPermissionService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _userPermissionMapping;
        #endregion

        #region Constructor
        public UserPermissionHandlerQuery(IUserPermissionService userPermissionService, IStringLocalizer<AppLocalization> stringLocalizer, IHttpContextAccessor httpContextAccessor, IMapper userPermissionMapping) : base(stringLocalizer)
        {
            _userPermissionService = userPermissionService;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _userPermissionMapping = userPermissionMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<UserPermissionFullDataDto>>> Handle(GetUserPermissionsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermissions = await _userPermissionService.GetAllAsync(cancellationToken);
            if (UserPermissions == null)
                return NotFound<List<UserPermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var UserPermissionsDto = _userPermissionMapping.Map<List<UserPermissionFullDataDto>>(UserPermissions).ToList();
            return Success(UserPermissionsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = UserPermissionsDto.Count });
        }

        public async Task<Response<UserPermissionFullDataDto>> Handle(GetUserPermissionByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermission = await _userPermissionService.GetByIdNoTrackingAsync(request.Id, cancellationToken);
            if (UserPermission == null)
                return NotFound<UserPermissionFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var UserPermissionDto = _userPermissionMapping.Map<UserPermissionFullDataDto>(UserPermission);
            return Success(UserPermissionDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<UserPermissionFullDataDto>>> Handle(GetUserPermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _userPermissionService.GetPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (PaginatedList == null)
                return NotFound<PaginatedList<UserPermissionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var UserPermissionFullDataDtoList = _userPermissionMapping.Map<List<UserPermissionFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<UserPermissionFullDataDto>.Success(UserPermissionFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        public async Task<Response<List<DropDown>>> Handle(GetUserPermissionsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermissions = await _userPermissionService.GetAllAsync(cancellationToken);
            if (UserPermissions == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _userPermissionMapping.Map<List<DropDown>>(UserPermissions).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion

    }
}
