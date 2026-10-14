using DGE.Core.Features.MetaTags.Commands.RequestModels;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.MetaTags.Commands.Validators
{
    public class UpdateMetaTagCommandValidator : AbstractValidator<UpdateMetaTagCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        #endregion

        #region Constructor
        public UpdateMetaTagCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Page)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleFor(x => x.Page)
                .Must(page => MetaTag.Pages.Contains(page!.Trim().ToLowerInvariant()))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist])
                .When(x => !string.IsNullOrWhiteSpace(x.Page));
        }
        #endregion
    }
}
