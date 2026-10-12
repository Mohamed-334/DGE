using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Products.Commands.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IProductService _productService;
        private readonly IVitaminService _vitaminService;
        private readonly IMineralService _mineralService;
        #endregion

        #region Constructor
        public AddProductCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IProductService productService, IVitaminService vitaminService, IMineralService mineralService)
        {
            _stringLocalizer = stringLocalizer;
            _productService = productService;
            _vitaminService = vitaminService;
            _mineralService = mineralService;
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
            RuleFor(x => x.ProductCategoryId)
                .NotEmpty().WithMessage(_stringLocalizer[AppLocalizationKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[AppLocalizationKeys.Required]);
        }
        public void ApplyCustomSignUpCommandValidation()
        {
            RuleForEach(x => x.HarvestMonthCodes)
                .Must(code => Enum.IsDefined(typeof(Month), code))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist]);
            RuleFor(x => x.VitaminIds)
                .MustAsync(async (ids, cancellationToken) => ids == null || ids.Count == 0 || await _vitaminService.AllExistAsync(ids))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist]);
            RuleFor(x => x.MineralIds)
                .MustAsync(async (ids, cancellationToken) => ids == null || ids.Count == 0 || await _mineralService.AllExistAsync(ids))
                .WithMessage(_stringLocalizer[AppLocalizationKeys.IsNotExist]);
        }
        #endregion
    }
}
