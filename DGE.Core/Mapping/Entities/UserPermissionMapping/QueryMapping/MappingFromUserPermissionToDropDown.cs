using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.UserPermissionMapping
{
    public partial class UserPermissionMapping
    {
        #region Methods
        public void MappingFromUserPermissionToDropDown()
        {
            CreateMap<UserPermission, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<UserPermission, DropDown>>();
        }
        #endregion
    }

}
