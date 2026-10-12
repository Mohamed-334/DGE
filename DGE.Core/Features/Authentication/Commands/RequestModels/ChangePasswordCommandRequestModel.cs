using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Authentication.Commands.RequestModels
{
    public class ChangePasswordCommandRequestModel : IRequest<Response<string>>
    {
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }

    }
}
