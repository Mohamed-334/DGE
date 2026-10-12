using DGE.Core.Features.ProductVarieties.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductVarieties.Commands.Validators
{
    public class DeleteProductVarietyCommandValidator : AbstractValidator<DeleteProductVarietyCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductVarietyService _productVarietyService;
        #endregion

        #region Constructor
        public DeleteProductVarietyCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductVarietyService productVarietyService)
        {
            _stringLocalizer = stringLocalizer;
            _productVarietyService = productVarietyService;
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
