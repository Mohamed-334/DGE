using BaseArchitecture.Core.Features.UserPermissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionsListQueryRequestModel : IRequest<Response<List<UserPermissionFullDataDto>>>
    {
    }
}
