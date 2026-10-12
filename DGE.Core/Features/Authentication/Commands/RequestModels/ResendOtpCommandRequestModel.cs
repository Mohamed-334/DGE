using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class ResendOtpCommandRequestModel : IRequest<Response<string>>
    {
        public string? Email { get; set; }
    }
}
