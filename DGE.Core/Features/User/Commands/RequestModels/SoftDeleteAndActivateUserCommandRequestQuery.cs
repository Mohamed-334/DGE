using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ApplicationUser.Commands.RequestModels
{
    public class SoftDeleteAndActivateUserCommandRequestQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
