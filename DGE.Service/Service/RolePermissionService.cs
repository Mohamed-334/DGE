using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class RolePermissionService : BaseService<RolePermission>, IRolePermissionService
    {
        #region Fields
        private readonly IRolePermissionRepository _rolePermissionRepository;
        #endregion

        #region Constructor
        public RolePermissionService(IStringLocalizer<AppLocalization> stringLocalizer, IRolePermissionRepository rolePermissionRepository, ILogger<BaseService<RolePermission>> logger)
            : base(rolePermissionRepository, stringLocalizer, logger)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }
        #endregion

        #region Methods
        #endregion
    }
}
