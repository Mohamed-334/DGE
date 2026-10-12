using BaseArchitecture.Core.Features.Permissions.Commands.RequestModels;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.PermissionMapping
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
