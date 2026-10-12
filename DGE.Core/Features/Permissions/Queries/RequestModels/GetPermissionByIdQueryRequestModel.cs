using DGE.Core.Features.Permissions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionByIdQueryRequestModel : IRequest<Response<PermissionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
