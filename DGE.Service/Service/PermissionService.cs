using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
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
