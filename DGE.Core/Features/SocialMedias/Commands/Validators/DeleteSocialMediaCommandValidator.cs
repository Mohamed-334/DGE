using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SocialMedias.Commands.Validators
{
    public class DeleteSocialMediaCommandValidator : AbstractValidator<DeleteSocialMediaCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ISocialMediaService _socialMediaService;
        #endregion

        #region Constructor
        public DeleteSocialMediaCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, ISocialMediaService socialMediaService)
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
