using AutoMapper;
using DGE.Core.Features.CompanySettings.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using CompanySettingsEntity = DGE.Domain.Entities.CompanySettings;

namespace DGE.Core.Features.CompanySettings.Commands.Handler
{
    public class CompanySettingsCommandHandler : ResponseHandler, IRequestHandler<UpdateCompanySettingsCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ICompanySettingsService _companySettingsService;
        #endregion

        #region Constructor
        public CompanySettingsCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  ICompanySettingsService companySettingsService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _companySettingsService = companySettingsService;
        }
        #endregion

        #region Methods
        // Creates the settings row on the first save, updates it afterwards
        public async Task<Response<string>> Handle(UpdateCompanySettingsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var CompanySettings = await _companySettingsService.GetAsync(asTracking: true);
            var isNew = CompanySettings == null;
            CompanySettings = _mapper.Map(request, CompanySettings ?? new CompanySettingsEntity());

            if (isNew)
            {
                var addResult = await _companySettingsService.AddAsync(CompanySettings);
                if (addResult == _stringLocalizer[AppLocalizationKeys.AddFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            }
            else
            {
                var editResult = await _companySettingsService.EditAsync(CompanySettings);
                if (editResult == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            }
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }
        #endregion
    }
}
