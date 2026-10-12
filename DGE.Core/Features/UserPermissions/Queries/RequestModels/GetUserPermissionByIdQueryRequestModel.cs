using DGE.Core.Features.UserPermissions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionByIdQueryRequestModel : IRequest<Response<UserPermissionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
