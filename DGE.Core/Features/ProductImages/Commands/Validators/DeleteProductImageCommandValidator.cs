using DGE.Core.Features.ProductImages.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductImages.Commands.Validators
{
    public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductImageService _productImageService;
        #endregion

        #region Constructor
        public DeleteProductImageCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductImageService productImageService)
        {
            _stringLocalizer = stringLocalizer;
            _productImageService = productImageService;
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
