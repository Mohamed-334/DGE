using DGE.Core.Features.Roles.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ApplicationUser.Queries.RequestModels
{
    public class GetUserRolesQueryRequestModel : IRequest<Response<List<RoleFullDataDto>>>
    {
        public int UserId { get; set; }
    }
}
