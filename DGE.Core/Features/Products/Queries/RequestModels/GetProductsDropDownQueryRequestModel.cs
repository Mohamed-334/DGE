using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Products.Queries.RequestModels
{
    public class GetProductsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
