using DGE.Core.Features.Permissions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionsListQueryRequestModel : IRequest<Response<List<PermissionFullDataDto>>>
    {
    }
}
