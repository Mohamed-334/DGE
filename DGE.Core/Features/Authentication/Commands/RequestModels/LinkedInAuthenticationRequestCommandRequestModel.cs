using BaseArchitecture.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace BaseArchitecture.Core.Features.Authentication.Commands.RequestModels
{
    public class LinkedInAuthenticationRequestCommandRequestModel : IRequest<Response<AuthenticationProperties>>
    {
        public string? RedirectUrl { get; set; }
    }
}
