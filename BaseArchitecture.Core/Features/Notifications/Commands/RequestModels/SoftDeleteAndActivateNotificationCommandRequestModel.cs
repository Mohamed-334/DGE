using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Notifications.Commands.RequestModels
{
    public class SoftDeleteAndActivateNotificationCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
