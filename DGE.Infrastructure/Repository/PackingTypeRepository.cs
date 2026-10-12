using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class PackingTypeRepository : BaseRepository<PackingType>, IPackingTypeRepository
    {
        #region Fields
        private readonly DbSet<PackingType> _packingType;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public PackingTypeRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _packingType = context.Set<PackingType>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
