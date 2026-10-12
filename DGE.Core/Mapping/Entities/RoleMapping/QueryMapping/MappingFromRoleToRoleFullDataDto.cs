using DGE.Core.Features.Roles.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.RoleMapping
{
    public partial class RoleMapping
    {
        #region Methods
        public void MappingFromRoleToRoleFullDataDto()
        {
            CreateMap<Role, RoleFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Role, RoleFullDataDto>>();
        }
        #endregion
    }

}
