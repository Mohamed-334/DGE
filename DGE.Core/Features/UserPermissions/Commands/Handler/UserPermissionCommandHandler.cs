using AutoMapper;
using DGE.Core.Features.UserPermissions.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.UserPermissions.Commands.Handler
{
    public class UserPermissionCommandHandler : ResponseHandler, IRequestHandler<AddUserPermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateUserPermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteUserPermissionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateUserPermissionCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public UserPermissionCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IUserPermissionService userPermissionService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userPermissionService = userPermissionService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermission = _mapper.Map<UserPermission>(request);
            var result = await _userPermissionService.AddAsync(UserPermission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = UserPermission.Id });
        }

        public async Task<Response<string>> Handle(UpdateUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermission = await _userPermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (UserPermission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var UserPermissionMapper = _mapper.Map(request, UserPermission);
            var result = await _userPermissionService.EditAsync(UserPermissionMapper, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermission = await _userPermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (UserPermission == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _userPermissionService.HardDeleteAsync(UserPermission, cancellationToken);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var UserPermission = await _userPermissionService.GetByIdAsync(request.Id, cancellationToken);
            if (UserPermission == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            UserPermission.IsDeleted = !(UserPermission.IsDeleted);
            UserPermission.DeletionDate = DateTime.UtcNow;
            UserPermission.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _userPermissionService.EditAsync(UserPermission, cancellationToken);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (UserPermission.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
