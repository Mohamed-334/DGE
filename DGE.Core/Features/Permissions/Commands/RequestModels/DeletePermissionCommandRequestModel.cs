using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Permissions.Commands.RequestModels
{
    public class DeletePermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
