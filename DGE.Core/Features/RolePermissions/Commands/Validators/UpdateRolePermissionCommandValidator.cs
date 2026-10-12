using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.RolePermissions.Commands.Validators
{
    public class UpdateRolePermissionCommandValidator : AbstractValidator<UpdateRolePermissionCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IRolePermissionService _rolePermissionService;
        #endregion

        #region Constructor
        public UpdateRolePermissionCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IRolePermissionService rolePermissionService)
        {
            _stringLocalizer = stringLocalizer;
            _rolePermissionService = rolePermissionService;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.PermissionId)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
