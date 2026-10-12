using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class ResetPasswordOtpVerificationCommandRequestModel : OtpVerificationCommandRequestModel, IRequest<Response<string>>
    {
    }
}
