using BaseArchitecture.Core.Features.Notifications.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationsListQueryRequestModel : IRequest<Response<List<NotificationFullDataDto>>>
    {
    }
}
