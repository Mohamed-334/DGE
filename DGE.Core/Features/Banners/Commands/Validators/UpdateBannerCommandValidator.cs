using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Banners.Commands.Validators
{
    public class UpdateBannerCommandValidator : AbstractValidator<UpdateBannerCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IBannerService _bannerService;
        #endregion

        #region Constructor
        public UpdateBannerCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IBannerService bannerService)
        {
            _stringLocalizer = stringLocalizer;
            _bannerService = bannerService;
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
