using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Permissions.Commands.RequestModels
{
    public class SoftDeleteAndActivatePermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
