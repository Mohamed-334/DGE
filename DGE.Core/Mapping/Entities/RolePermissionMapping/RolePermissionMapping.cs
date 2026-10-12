using AutoMapper;

namespace BaseArchitecture.Core.Mapping.RolePermissionMapping
{
    public partial class RolePermissionMapping : Profile
    {
        #region Constructor
        public RolePermissionMapping()
        {
            MapFromAddRolePermissionCommandRequestModelToRolePermissionEntity();
            MapFromUpdateRolePermissionCommandRequestModelToRolePermissionEntity();
            MappingFromRolePermissionToRolePermissionFullDataDto();
            MappingFromRolePermissionToDropDown();
        }
        #endregion

    }
}
