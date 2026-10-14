using DGE.Core.Features.SiteSections.Commands.RequestModels;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SiteSections.Commands.Validators
{
    public class UpdateSiteSectionCommandValidator : AbstractValidator<UpdateSiteSectionCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        #endregion

        #region Constructor
        public UpdateSiteSectionCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Key)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleFor(x => x.Key)
                .Must(key => SiteSection.Keys.Contains(key!.Trim().ToLowerInvariant()))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist])
                .When(x => !string.IsNullOrWhiteSpace(x.Key));
        }
        #endregion
    }
}
