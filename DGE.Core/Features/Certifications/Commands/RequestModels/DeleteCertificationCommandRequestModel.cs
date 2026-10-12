using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Certifications.Commands.RequestModels
{
    public class DeleteCertificationCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
