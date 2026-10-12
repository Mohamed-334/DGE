using DGE.Core.Features.Roles.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RoleMapping
{
    public partial class RoleMapping
    {
        #region Methods
        public void MapFromAddRoleCommandRequestModelToRoleEntity()
        {
            CreateMap<AddRoleCommandRequestModel, Role>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddRoleCommandRequestModel, Role>>();
            #endregion
        }
    }
}
