using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.Context;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.BaseRepository;
using BaseArchitecture.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BaseArchitecture.Infrastructure.Repository
{
    public class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
    {
        #region Fields
        private readonly DbSet<RolePermission> _rolePermission;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public RolePermissionRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _rolePermission = context.Set<RolePermission>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
