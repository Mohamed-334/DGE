using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.PermissionMapping
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
