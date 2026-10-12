using AutoMapper;
using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.RolePermissions.Commands.Handler
{
    public class RolePermissionCommandHandler : ResponseHandler, IRequestHandler<AddRolePermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateRolePermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteRolePermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateRolePermissionCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public RolePermissionCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IRolePermissionService rolePermissionService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _rolePermissionService = rolePermissionService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermission = _mapper.Map<RolePermission>(request);
            var result = await _rolePermissionService.AddAsync(RolePermission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = RolePermission.Id });
        }

        public async Task<Response<string>> Handle(UpdateRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermission = await _rolePermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (RolePermission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var RolePermissionMapper = _mapper.Map(request, RolePermission);
            var result = await _rolePermissionService.EditAsync(RolePermissionMapper, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermission = await _rolePermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (RolePermission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _rolePermissionService.HardDeleteAsync(RolePermission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var RolePermission = await _rolePermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (RolePermission == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            RolePermission.IsDeleted = !(RolePermission.IsDeleted);
            RolePermission.DeletionDate = DateTime.UtcNow;
            RolePermission.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _rolePermissionService.EditAsync(RolePermission, cancellationToken);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (RolePermission.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
