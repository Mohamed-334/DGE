using DGE.Core.Features.RolePermissions.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RolePermissionMapping
{
    public partial class RolePermissionMapping
    {
        #region Methods
        public void MappingFromRolePermissionToRolePermissionFullDataDto()
        {
            CreateMap<RolePermission, RolePermissionFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<RolePermission, RolePermissionFullDataDto>>();
        }
        #endregion
    }

}
