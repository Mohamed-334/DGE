using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
