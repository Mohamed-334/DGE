using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Queries.RequestModels
{
    public class GetProductVitaminsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
