using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SocialMedias.Commands.Validators
{
    public class AddSocialMediaCommandValidator : AbstractValidator<AddSocialMediaCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ISocialMediaService _socialMediaService;
        #endregion

        #region Constructor
        public AddSocialMediaCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, ISocialMediaService socialMediaService)
        {
            _stringLocalizer = stringLocalizer;
            _socialMediaService = socialMediaService;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Platform)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleFor(x => x.Platform)
                .Must(p => SocialMedia.Platforms.Contains(p!.Trim(), StringComparer.OrdinalIgnoreCase))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist])
                .When(x => !string.IsNullOrWhiteSpace(x.Platform));
            RuleFor(x => x.Url)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.InvalidUrl])
                .When(x => !string.IsNullOrWhiteSpace(x.Url));
        }
        #endregion
    }
}
