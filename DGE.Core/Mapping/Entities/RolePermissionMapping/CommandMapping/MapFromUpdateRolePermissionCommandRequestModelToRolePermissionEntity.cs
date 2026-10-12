using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RolePermissionMapping
{
    public partial class RolePermissionMapping
    {
        #region Methods
        public void MapFromUpdateRolePermissionCommandRequestModelToRolePermissionEntity()
        {
            CreateMap<UpdateRolePermissionCommandRequestModel, RolePermission>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateRolePermissionCommandRequestModel, RolePermission>>();
        }
        #endregion
    }
}
