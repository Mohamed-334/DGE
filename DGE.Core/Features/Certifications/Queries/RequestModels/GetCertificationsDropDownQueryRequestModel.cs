using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Certifications.Queries.RequestModels
{
    public class GetCertificationsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
