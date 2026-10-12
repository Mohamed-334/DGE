using BaseArchitecture.Core.Features.Notifications.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationByIdQueryRequestModel : IRequest<Response<NotificationFullDataDto>>
    {
        public int Id { get; set; }

    }
}
