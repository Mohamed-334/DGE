using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace BaseArchitecture.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
