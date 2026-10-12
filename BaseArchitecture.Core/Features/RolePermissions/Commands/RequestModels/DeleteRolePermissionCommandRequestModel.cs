using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.RolePermissions.Commands.RequestModels
{
    public class DeleteRolePermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
