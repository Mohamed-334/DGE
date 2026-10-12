using DGE.Core.Features.Types.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Types.Commands.Validators
{
    public class AddTypeCommandValidator : AbstractValidator<AddTypeCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ITypeService _typeService;
        #endregion

        #region Constructor
        public AddTypeCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, ITypeService typeService)
        {
            _stringLocalizer = stringLocalizer;
            _typeService = typeService;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
