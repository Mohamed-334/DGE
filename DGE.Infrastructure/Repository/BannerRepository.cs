using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class BannerRepository : BaseRepository<Banner>, IBannerRepository
    {
        #region Fields
        private readonly DbSet<Banner> _banner;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public BannerRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _banner = context.Set<Banner>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
