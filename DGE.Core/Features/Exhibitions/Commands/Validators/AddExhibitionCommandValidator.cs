using DGE.Core.Features.Exhibitions.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Exhibitions.Commands.Validators
{
    public class AddExhibitionCommandValidator : AbstractValidator<AddExhibitionCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IExhibitionService _exhibitionService;
        #endregion

        #region Constructor
        public AddExhibitionCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IExhibitionService exhibitionService)
        {
            _stringLocalizer = stringLocalizer;
            _exhibitionService = exhibitionService;
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
