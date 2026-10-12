using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.RolePermissionMapping
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
