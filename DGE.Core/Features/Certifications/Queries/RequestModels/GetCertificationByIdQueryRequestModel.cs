using DGE.Core.Features.Certifications.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Certifications.Queries.RequestModels
{
    public class GetCertificationByIdQueryRequestModel : IRequest<Response<CertificationFullDataDto>>
    {
        public int Id { get; set; }
    }
}
