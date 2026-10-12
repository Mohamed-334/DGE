using DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductHarvestMonths.Commands.Validators
{
    public class DeleteProductHarvestMonthCommandValidator : AbstractValidator<DeleteProductHarvestMonthCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductHarvestMonthService _productHarvestMonthService;
        #endregion

        #region Constructor
        public DeleteProductHarvestMonthCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductHarvestMonthService productHarvestMonthService)
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
