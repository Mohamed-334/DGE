using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Authentication.Commands.RequestModels
{
    public class LogoutCommandRequestModel : IRequest<Response<string>>
    {
    }
}
