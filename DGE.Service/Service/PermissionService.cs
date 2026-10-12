using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.Localization;
using BaseArchitecture.Service.ServiceInterfaces;
using BaseArchitecture.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Service.Service
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        #region Fields
        private readonly IPermissionRepository _permissionRepository;
        #endregion

        #region Constructor
        public PermissionService(IStringLocalizer<AppLocalization> stringLocalizer, IPermissionRepository permissionRepository, ILogger<BaseService<Permission>> logger)
            : base(permissionRepository, stringLocalizer, logger)
        {
            _permissionRepository = permissionRepository;
        }
        #endregion

        #region Methods
        #endregion
    }
}
