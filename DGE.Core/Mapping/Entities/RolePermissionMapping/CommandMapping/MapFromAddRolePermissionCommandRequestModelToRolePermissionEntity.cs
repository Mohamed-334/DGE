using BaseArchitecture.Core.Features.RolePermissions.Commands.RequestModels;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.RolePermissionMapping
{
    public partial class RolePermissionMapping
    {
        #region Methods
        public void MapFromAddRolePermissionCommandRequestModelToRolePermissionEntity()
        {
            CreateMap<AddRolePermissionCommandRequestModel, RolePermission>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddRolePermissionCommandRequestModel, RolePermission>>();
        }
        #endregion
    }
}
