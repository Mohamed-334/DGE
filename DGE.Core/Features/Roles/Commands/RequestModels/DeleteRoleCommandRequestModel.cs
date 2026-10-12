using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Roles.Commands.RequestModels
{
    public class DeleteRoleCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
