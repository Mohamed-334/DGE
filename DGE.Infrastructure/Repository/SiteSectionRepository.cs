using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class SiteSectionRepository : BaseRepository<SiteSection>, ISiteSectionRepository
    {
        #region Fields
        private readonly DbSet<SiteSection> _siteSection;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public SiteSectionRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _siteSection = context.Set<SiteSection>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
