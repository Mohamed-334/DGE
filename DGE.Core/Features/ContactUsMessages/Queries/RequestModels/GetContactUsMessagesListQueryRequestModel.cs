using DGE.Core.Features.ContactUsMessages.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ContactUsMessages.Queries.RequestModels
{
    public class GetContactUsMessagesListQueryRequestModel : IRequest<Response<List<ContactUsFullDataDto>>>
    {
    }
}
