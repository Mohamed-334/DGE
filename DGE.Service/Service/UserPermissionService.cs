using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.Localization;
using BaseArchitecture.Service.ServiceInterfaces;
using BaseArchitecture.Service.Shared.BaseService;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Service.Service
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
