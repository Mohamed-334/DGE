using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.Localization;
using BaseArchitecture.Service.ServiceInterfaces;
using BaseArchitecture.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Service.Service
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
