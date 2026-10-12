using DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.ProductHarvestMonths.Commands.Validators
{
    public class AddProductHarvestMonthCommandValidator : AbstractValidator<AddProductHarvestMonthCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductHarvestMonthService _productHarvestMonthService;
        #endregion

        #region Constructor
        public AddProductHarvestMonthCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductHarvestMonthService productHarvestMonthService)
        {
            _stringLocalizer = stringLocalizer;
            _productHarvestMonthService = productHarvestMonthService;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.MonthStatusCode)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleFor(x => x.MonthStatusCode)
                .Must(code => code == null || Enum.IsDefined(typeof(Month), code.Value))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist]);
        }
        #endregion
    }
}
