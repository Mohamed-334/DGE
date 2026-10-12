using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ContactUsMessages.Commands.RequestModels
{
    public class AddContactUsCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
