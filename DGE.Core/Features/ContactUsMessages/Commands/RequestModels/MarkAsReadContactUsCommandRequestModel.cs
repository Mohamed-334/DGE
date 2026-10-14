using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ContactUsMessages.Commands.RequestModels
{
    public class MarkAsReadContactUsCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
