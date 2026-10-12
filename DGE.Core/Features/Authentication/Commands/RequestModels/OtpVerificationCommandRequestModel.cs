using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class OtpVerificationCommandRequestModel : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }
}
