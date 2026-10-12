using DGE.Core.Features.Certifications.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Certifications.Queries.RequestModels
{
    public class GetCertificationsListQueryRequestModel : IRequest<Response<List<CertificationFullDataDto>>>
    {
    }
}
