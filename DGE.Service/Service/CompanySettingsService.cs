using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class CompanySettingsService : BaseService<CompanySettings>, ICompanySettingsService
    {
        #region Fields
        private readonly ICompanySettingsRepository _companySettingsRepository;
        #endregion

        #region Constructor
        public CompanySettingsService(IStringLocalizer<AppLocalization> stringLocalizer, ICompanySettingsRepository companySettingsRepository, ILogger<BaseService<CompanySettings>> logger)
            : base(companySettingsRepository, stringLocalizer, logger)
        {
            _companySettingsRepository = companySettingsRepository;
        }
        #endregion

        #region Methods
        // There is only one settings row; the oldest one wins if more were ever created
        public async Task<CompanySettings?> GetAsync(bool asTracking = false)
        {
            var queryable = asTracking ? _companySettingsRepository.GetTableAsTracking() : _companySettingsRepository.GetTableNoTracking();
            return await queryable.OrderBy(c => c.Id).FirstOrDefaultAsync();
        }
        #endregion
    }
}
