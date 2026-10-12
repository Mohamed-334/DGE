using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ContactUsMessages.Commands.Validators
{
    public class DeleteContactUsCommandValidator : AbstractValidator<DeleteContactUsCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IContactUsService _contactUsService;
        #endregion

        #region Constructor
        public DeleteContactUsCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, IContactUsService contactUsService)
        {
            _stringLocalizer = stringLocalizer;
            _contactUsService = contactUsService;
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
