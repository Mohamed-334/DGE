using DGE.Core.Features.PackingTypes.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.PackingTypes.Commands.Validators
{
    public class UpdatePackingTypeCommandValidator : AbstractValidator<UpdatePackingTypeCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IPackingTypeService _packingTypeService;
        #endregion

        #region Constructor
        public UpdatePackingTypeCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IPackingTypeService packingTypeService)
        {
            _stringLocalizer = stringLocalizer;
            _packingTypeService = packingTypeService;
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
        }
        public void ApplyCustomSignUpCommandValidation()
        {
        }
        #endregion
    }
}
