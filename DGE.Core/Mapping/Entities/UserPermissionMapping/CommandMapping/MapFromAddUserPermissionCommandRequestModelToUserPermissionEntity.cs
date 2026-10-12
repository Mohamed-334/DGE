using DGE.Core.Features.UserPermissions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.UserPermissionMapping
{
    public partial class UserPermissionMapping
    {
        #region Methods
        public void MapFromAddUserPermissionCommandRequestModelToUserPermissionEntity()
        {
            CreateMap<AddUserPermissionCommandRequestModel, UserPermission>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddUserPermissionCommandRequestModel, UserPermission>>();
        }
        #endregion
    }
}
