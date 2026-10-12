using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Core.Mapping.PermissionMapping
{
    public partial class PermissionMapping
    {
        #region Methods
        public void MappingFromPermissionToDropDown()
        {
            CreateMap<Permission, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Permission, DropDown>>();
        }
        #endregion
    }

}
