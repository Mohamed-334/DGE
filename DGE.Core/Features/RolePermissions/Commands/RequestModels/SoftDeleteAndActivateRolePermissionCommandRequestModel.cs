using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.RolePermissions.Commands.RequestModels
{
    public class SoftDeleteAndActivateRolePermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
