using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RolePermissionMapping
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
