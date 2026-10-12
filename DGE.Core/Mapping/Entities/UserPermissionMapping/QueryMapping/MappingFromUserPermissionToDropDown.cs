using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Core.Mapping.UserPermissionMapping
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
