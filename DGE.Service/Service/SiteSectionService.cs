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
    public class SiteSectionService : BaseService<SiteSection>, ISiteSectionService
    {
        #region Fields
        private readonly ISiteSectionRepository _siteSectionRepository;
        #endregion

        #region Constructor
        public SiteSectionService(IStringLocalizer<AppLocalization> stringLocalizer, ISiteSectionRepository siteSectionRepository, ILogger<BaseService<SiteSection>> logger)
            : base(siteSectionRepository, stringLocalizer, logger)
        {
            _siteSectionRepository = siteSectionRepository;
        }
        #endregion

        #region Methods
        public async Task<SiteSection?> GetByKeyAsync(string key, bool asTracking = false)
        {
            var queryable = asTracking ? _siteSectionRepository.GetTableAsTracking() : _siteSectionRepository.GetTableNoTracking();
            return await queryable
                            .Include(s => s.Items)
                            .FirstOrDefaultAsync(s => s.Key == key);
        }
        #endregion
    }
}
