using DGE.Core.Features.RolePermissions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.RolePermissions.Queries.RequestModels
{
    public class GetRolePermissionsListQueryRequestModel : IRequest<Response<List<RolePermissionFullDataDto>>>
    {
    }
}
