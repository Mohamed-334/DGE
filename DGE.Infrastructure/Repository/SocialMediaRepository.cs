using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class SocialMediaRepository : BaseRepository<SocialMedia>, ISocialMediaRepository
    {
        #region Fields
        private readonly DbSet<SocialMedia> _socialMedia;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public SocialMediaRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _socialMedia = context.Set<SocialMedia>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
