using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Permissions.Commands.RequestModels
{
    public class UpdatePermissionCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public int PermissionNumber { get; set; }
    }
}
