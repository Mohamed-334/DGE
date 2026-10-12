using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Core.Mapping.RolePermissionMapping
{
    public partial class RolePermissionMapping
    {
        #region Methods
        public void MappingFromRolePermissionToDropDown()
        {
            CreateMap<RolePermission, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<RolePermission, DropDown>>();
        }
        #endregion
    }

}
