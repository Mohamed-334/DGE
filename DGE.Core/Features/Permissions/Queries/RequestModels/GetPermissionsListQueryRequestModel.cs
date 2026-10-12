using BaseArchitecture.Core.Features.Permissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionsListQueryRequestModel : IRequest<Response<List<PermissionFullDataDto>>>
    {
    }
}
