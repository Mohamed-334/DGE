using DGE.Core.Features.Permissions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.PermissionMapping
{
    public partial class PermissionMapping
    {
        #region Methods
        public void MapFromAddPermissionCommandRequestModelToPermissionEntity()
        {
            CreateMap<AddPermissionCommandRequestModel, Permission>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddPermissionCommandRequestModel, Permission>>();
        }
        #endregion
    }
}
