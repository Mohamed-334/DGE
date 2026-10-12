using DGE.Core.Features.Vitamins.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Vitamins.Commands.Validators
{
    public class DeleteVitaminCommandValidator : AbstractValidator<DeleteVitaminCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IVitaminService _vitaminService;
        #endregion

        #region Constructor
        public DeleteVitaminCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IVitaminService vitaminService)
        {
            _stringLocalizer = stringLocalizer;
            _vitaminService = vitaminService;
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
