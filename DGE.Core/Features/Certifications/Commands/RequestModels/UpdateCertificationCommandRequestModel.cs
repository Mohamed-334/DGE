using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Certifications.Commands.RequestModels
{
    public class UpdateCertificationCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public IFormFile? CertificationUrlFile { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
    }
}
