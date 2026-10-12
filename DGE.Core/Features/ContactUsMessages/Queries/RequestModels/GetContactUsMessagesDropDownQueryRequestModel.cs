using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ContactUsMessages.Queries.RequestModels
{
    public class GetContactUsMessagesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
