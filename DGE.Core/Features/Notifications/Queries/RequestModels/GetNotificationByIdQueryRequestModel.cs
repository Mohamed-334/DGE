using DGE.Core.Features.Notifications.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationByIdQueryRequestModel : IRequest<Response<NotificationFullDataDto>>
    {
        public int Id { get; set; }

    }
}
