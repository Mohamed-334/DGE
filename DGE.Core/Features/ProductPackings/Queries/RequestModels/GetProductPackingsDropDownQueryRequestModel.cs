using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Queries.RequestModels
{
    public class GetProductPackingsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
