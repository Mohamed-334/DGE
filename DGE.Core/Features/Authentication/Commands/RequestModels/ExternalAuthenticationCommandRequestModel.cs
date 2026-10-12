using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class ExternalAuthenticationCommandRequestModel : IRequest<Response<string>>
    {
    }
}
