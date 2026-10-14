using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.ExtensionMethods;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Products.Commands.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public UpdateProductCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductService productService)
        {
            _stringLocalizer = stringLocalizer;
            _productService = productService;
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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
            RuleFor(x => x.ProductCategoryId)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleFor(x => x.Slug)
                .Must(SlugGenerator.IsValid).WithMessage(_stringLocalizer[AppLocalizationKeys.InvalidSlug])
                .MustAsync(async (model, slug, cancellationToken) => !await _productService.IsSlugExistAsync(slug!, model.Id))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsExist])
                .When(x => !string.IsNullOrWhiteSpace(x.Slug));
        }
        #endregion
    }
}
