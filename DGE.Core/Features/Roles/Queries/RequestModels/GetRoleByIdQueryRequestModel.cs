using DGE.Core.Features.Roles.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Roles.Queries.RequestModels
{
    public class GetRoleByIdQueryRequestModel : IRequest<Response<RoleFullDataDto>>
    {
        public int RoleId { get; set; }

    }
}
