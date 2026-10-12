using BaseArchitecture.Core.Features.RolePermissions.Dto;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.RolePermissionMapping
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
