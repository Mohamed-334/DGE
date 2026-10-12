using DGE.Core.Features.ProductMinerals.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductMinerals.Commands.Validators
{
    public class DeleteProductMineralCommandValidator : AbstractValidator<DeleteProductMineralCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductMineralService _productMineralService;
        #endregion

        #region Constructor
        public DeleteProductMineralCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductMineralService productMineralService)
        {
            _stringLocalizer = stringLocalizer;
            _productMineralService = productMineralService;
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
