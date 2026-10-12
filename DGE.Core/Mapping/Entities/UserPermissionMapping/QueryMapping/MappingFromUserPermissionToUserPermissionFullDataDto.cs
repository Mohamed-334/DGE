using BaseArchitecture.Core.Features.UserPermissions.Dto;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.UserPermissionMapping
{
    public partial class UserPermissionMapping
    {
        #region Methods
        public void MappingFromUserPermissionToUserPermissionFullDataDto()
        {
            CreateMap<UserPermission, UserPermissionFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<UserPermission, UserPermissionFullDataDto>>();
        }
        #endregion
    }

}
