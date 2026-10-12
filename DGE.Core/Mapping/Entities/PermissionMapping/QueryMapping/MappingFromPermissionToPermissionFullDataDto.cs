using BaseArchitecture.Core.Features.Permissions.Dto;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.PermissionMapping
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
