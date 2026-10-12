using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.UserPermissions.Commands.RequestModels
{
    public class DeleteUserPermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
