using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Notifications.Commands.RequestModels
{
    public class DeleteNotificationCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
