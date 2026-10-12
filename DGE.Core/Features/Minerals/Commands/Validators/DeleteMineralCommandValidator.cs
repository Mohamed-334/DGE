using DGE.Core.Features.Minerals.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Minerals.Commands.Validators
{
    public class DeleteMineralCommandValidator : AbstractValidator<DeleteMineralCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMineralService _mineralService;
        #endregion

        #region Constructor
        public DeleteMineralCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IMineralService mineralService)
        {
            _stringLocalizer = stringLocalizer;
            _mineralService = mineralService;
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
