using BaseArchitecture.Core.Features.RolePermissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.RolePermissions.Queries.RequestModels
{
    public class GetRolePermissionByIdQueryRequestModel : IRequest<Response<RolePermissionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
