using DGE.Core.Features.Roles.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Roles.Queries.RequestModels
{
    public class GetRolesListQueryRequestModel : IRequest<Response<List<RoleFullDataDto>>>
    {
    }
}
