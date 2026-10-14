using DGE.Core.Features.CompanySettings.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.CompanySettings.Commands.Validators
{
    public class UpdateCompanySettingsCommandValidator : AbstractValidator<UpdateCompanySettingsCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        #endregion

        #region Constructor
        public UpdateCompanySettingsCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleForEach(x => x.Emails)
                .EmailAddress().WithMessage(_stringLocalizer[AppLocalizationKeys.InvalidEmail])
                .When(x => x.Emails != null);
            RuleFor(x => x.NotificationEmail)
                .EmailAddress().WithMessage(_stringLocalizer[AppLocalizationKeys.InvalidEmail])
                .When(x => !string.IsNullOrWhiteSpace(x.NotificationEmail));
            RuleFor(x => x.MapUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.InvalidUrl])
                .When(x => !string.IsNullOrWhiteSpace(x.MapUrl));
        }
        #endregion
    }
}
