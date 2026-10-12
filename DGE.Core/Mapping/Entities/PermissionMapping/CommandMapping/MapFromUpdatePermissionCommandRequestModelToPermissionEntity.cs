using DGE.Core.Features.Permissions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.PermissionMapping
{
    public partial class PermissionMapping
    {
        #region Methods
        public void MapFromUpdatePermissionCommandRequestModelToPermissionEntity()
        {
            CreateMap<UpdatePermissionCommandRequestModel, Permission>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdatePermissionCommandRequestModel, Permission>>();
        }
        #endregion
    }
}
