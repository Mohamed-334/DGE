using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class UserPermissionService : BaseService<UserPermission>, IUserPermissionService
    {
        #region Fields
        private readonly IUserPermissionRepository _userPermissionRepository;
        #endregion

        #region Constructor
        public UserPermissionService(IStringLocalizer<AppLocalization> stringLocalizer, IUserPermissionRepository userPermissionRepository, ILogger<BaseService<UserPermission>> logger)
            : base(userPermissionRepository, stringLocalizer, logger)
        {
            _userPermissionRepository = userPermissionRepository;
        }
        #endregion

        #region Methods
        #endregion
    }
}
