using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Commands.RequestModels
{
    public class SoftDeleteAndActivateUserPermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
