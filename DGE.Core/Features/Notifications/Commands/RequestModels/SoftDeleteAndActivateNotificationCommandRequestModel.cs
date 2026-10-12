using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Notifications.Commands.RequestModels
{
    public class SoftDeleteAndActivateNotificationCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
