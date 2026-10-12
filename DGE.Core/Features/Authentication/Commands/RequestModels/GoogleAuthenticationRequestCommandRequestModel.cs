using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class GoogleAuthenticationRequestCommandRequestModel : IRequest<Response<AuthenticationProperties>>
    {
        public string? RedirectUrl { get; set; }
    }
}
