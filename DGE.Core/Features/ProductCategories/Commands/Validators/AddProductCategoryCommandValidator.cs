using DGE.Core.Features.ProductCategories.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductCategories.Commands.Validators
{
    public class AddProductCategoryCommandValidator : AbstractValidator<AddProductCategoryCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductCategoryService _productCategoryService;
        #endregion

        #region Constructor
        public AddProductCategoryCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductCategoryService productCategoryService)
        {
            _stringLocalizer = stringLocalizer;
            _productCategoryService = productCategoryService;
            ApplySignUpCommandValidation();
            ApplyCustomSignUpCommandValidation();
        }
        #endregion

        #region Methods
        public void ApplySignUpCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
