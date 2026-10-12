using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.RolePermissions.Commands.Validators
{
    public class DeleteRolePermissionCommandValidator : AbstractValidator<DeleteRolePermissionCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IRolePermissionService _rolePermissionService;
        #endregion

        #region Constructor
        public DeleteRolePermissionCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IRolePermissionService rolePermissionService)
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
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
