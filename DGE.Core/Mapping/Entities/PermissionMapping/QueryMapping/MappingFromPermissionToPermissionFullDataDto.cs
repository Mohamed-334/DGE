using DGE.Core.Features.Permissions.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.PermissionMapping
{
    public partial class PermissionMapping
    {
        #region Methods
        public void MappingFromPermissionToPermissionFullDataDto()
        {
            CreateMap<Permission, PermissionFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Permission, PermissionFullDataDto>>();
        }
        #endregion
    }

}
