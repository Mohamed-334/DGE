using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class MineralRepository : BaseRepository<Mineral>, IMineralRepository
    {
        #region Fields
        private readonly DbSet<Mineral> _mineral;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public MineralRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _mineral = context.Set<Mineral>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
