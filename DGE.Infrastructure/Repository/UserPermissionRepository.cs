using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class UserPermissionRepository : BaseRepository<UserPermission>, IUserPermissionRepository
    {
        #region Fields
        private readonly DbSet<UserPermission> _userPermission;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public UserPermissionRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _userPermission = context.Set<UserPermission>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
