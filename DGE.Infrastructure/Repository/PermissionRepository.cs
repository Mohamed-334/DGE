using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.Context;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.BaseRepository;
using BaseArchitecture.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BaseArchitecture.Infrastructure.Repository
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        #region Fields
        private readonly DbSet<Permission> _permission;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public PermissionRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _permission = context.Set<Permission>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
