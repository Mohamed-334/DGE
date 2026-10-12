using BaseArchitecture.Core.Features.UserPermissions.Commands.RequestModels;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.UserPermissionMapping
{
    public partial class UserPermissionMapping
    {
        #region Methods
        public void MapFromAddUserPermissionCommandRequestModelToUserPermissionEntity()
        {
            CreateMap<AddUserPermissionCommandRequestModel, UserPermission>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddUserPermissionCommandRequestModel, UserPermission>>();
        }
        #endregion
    }
}
