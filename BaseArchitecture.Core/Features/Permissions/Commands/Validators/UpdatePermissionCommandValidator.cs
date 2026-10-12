using BaseArchitecture.Core.Features.Permissions.Commands.RequestModels;
using BaseArchitecture.Infrastructure.Shared.Localization;
using BaseArchitecture.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BaseArchitecture.Core.Features.Permissions.Commands.Validators
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IPermissionService _permissionService;
        #endregion

        #region Constructor
        public UpdatePermissionCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IPermissionService permissionService)
        {
            _stringLocalizer = stringLocalizer;
            _permissionService = permissionService;
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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.PermissionNumber)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
