using DGE.Core.Features.UserPermissions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionsListQueryRequestModel : IRequest<Response<List<UserPermissionFullDataDto>>>
    {
    }
}
