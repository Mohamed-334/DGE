using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class MetaTagRepository : BaseRepository<MetaTag>, IMetaTagRepository
    {
        #region Fields
        private readonly DbSet<MetaTag> _metaTag;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public MetaTagRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _metaTag = context.Set<MetaTag>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
