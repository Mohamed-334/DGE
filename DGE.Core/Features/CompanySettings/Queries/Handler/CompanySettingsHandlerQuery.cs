using AutoMapper;
using DGE.Core.Features.CompanySettings.Dto;
using DGE.Core.Features.CompanySettings.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.CompanySettings.Queries.Handler
{
    public class CompanySettingsHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetCompanySettingsQueryRequestModel, Response<CompanySettingsFullDataDto>>
    {
        #region Fields
        private readonly ICompanySettingsService _companySettingsService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _companySettingsMapping;
        #endregion

        #region Constructor
        public CompanySettingsHandlerQuery(ICompanySettingsService companySettingsService, IAuthenticatedUserService authenticatedUserService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper companySettingsMapping) : base(stringLocalizer)
        {
            _companySettingsService = companySettingsService;
            _authenticatedUserService = authenticatedUserService;
            _stringLocalizer = stringLocalizer;
            _companySettingsMapping = companySettingsMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<CompanySettingsFullDataDto>> Handle(GetCompanySettingsQueryRequestModel request, CancellationToken cancellationToken)
        {
            // Not saved yet: empty settings instead of 404 so the footer still renders
            var CompanySettings = await _companySettingsService.GetAsync();
            var CompanySettingsDto = CompanySettings == null
                ? new CompanySettingsFullDataDto()
                : _companySettingsMapping.Map<CompanySettingsFullDataDto>(CompanySettings);
            // The notification inbox is internal; the public site never needs it
            if (!_authenticatedUserService.IsAuthenticated())
                CompanySettingsDto.NotificationEmail = null;
            return Success(CompanySettingsDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
