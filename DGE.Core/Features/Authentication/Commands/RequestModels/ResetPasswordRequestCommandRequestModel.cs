using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class ResetPasswordRequestCommandRequestModel : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
