using AutoMapper;
using DGE.Core.Features.Permissions.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Permissions.Commands.Handler
{
    public class PermissionCommandHandler : ResponseHandler, IRequestHandler<AddPermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdatePermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeletePermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivatePermissionCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public PermissionCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IPermissionService permissionService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _permissionService = permissionService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Permission = _mapper.Map<Permission>(request);
            var result = await _permissionService.AddAsync(Permission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Permission.Id });
        }

        public async Task<Response<string>> Handle(UpdatePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Permission = await _permissionService.GetByIdAsync(request.Id, cancellationToken);
            if (Permission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var PermissionMapper = _mapper.Map(request, Permission);
            var result = await _permissionService.EditAsync(PermissionMapper, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeletePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Permission = await _permissionService.GetByIdAsync(request.Id, cancellationToken);
            if (Permission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _permissionService.HardDeleteAsync(Permission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivatePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Permission = await _permissionService.GetByIdAsync(request.Id, cancellationToken);
            if (Permission == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Permission.IsDeleted = !(Permission.IsDeleted);
            Permission.DeletionDate = DateTime.UtcNow;
            Permission.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _permissionService.EditAsync(Permission, cancellationToken);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Permission.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
