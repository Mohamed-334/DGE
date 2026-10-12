using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class CertificationRepository : BaseRepository<Certification>, ICertificationRepository
    {
        #region Fields
        private readonly DbSet<Certification> _certification;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public CertificationRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _certification = context.Set<Certification>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
