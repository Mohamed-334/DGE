using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Banners.Commands.Validators
{
    public class AddBannerCommandValidator : AbstractValidator<AddBannerCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IBannerService _bannerService;
        #endregion

        #region Constructor
        public AddBannerCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IBannerService bannerService)
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
            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
