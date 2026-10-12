using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Queries.RequestModels
{
    public class GetProductMineralsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
