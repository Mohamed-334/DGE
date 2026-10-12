using DGE.Core.Features.UserPermissions.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.UserPermissionMapping
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
