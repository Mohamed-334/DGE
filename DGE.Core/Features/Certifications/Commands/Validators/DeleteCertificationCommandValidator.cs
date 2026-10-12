using DGE.Core.Features.Certifications.Commands.RequestModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Certifications.Commands.Validators
{
    public class DeleteCertificationCommandValidator : AbstractValidator<DeleteCertificationCommandRequestModel>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ICertificationService _certificationService;
        #endregion

        #region Constructor
        public DeleteCertificationCommandValidator(IStringLocalizer<AppLocalization> stringLocalizer, ICertificationService certificationService)
        {
            _stringLocalizer = stringLocalizer;
            _certificationService = certificationService;
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
