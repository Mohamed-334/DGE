using DGE.Core.Features.Roles.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RoleMapping
{
    public partial class RoleMapping
    {
        #region Methods
        public void MapFromUpdateRoleCommandRequestModelToRoleEntity()
        {
            CreateMap<UpdateRoleCommandRequestModel, Role>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateRoleCommandRequestModel, Role>>();
        }
        #endregion
    }
}
