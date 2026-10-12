using DGE.Core.Features.ProductTypes.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductTypes.Commands.Validators
{
    public class DeleteProductTypeCommandValidator : AbstractValidator<DeleteProductTypeCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductTypeService _productTypeService;
        #endregion

        #region Constructor
        public DeleteProductTypeCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductTypeService productTypeService)
        {
            _stringLocalizer = stringLocalizer;
            _productTypeService = productTypeService;
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
