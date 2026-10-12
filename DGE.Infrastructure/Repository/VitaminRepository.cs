using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class VitaminRepository : BaseRepository<Vitamin>, IVitaminRepository
    {
        #region Fields
        private readonly DbSet<Vitamin> _vitamin;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public VitaminRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _vitamin = context.Set<Vitamin>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
