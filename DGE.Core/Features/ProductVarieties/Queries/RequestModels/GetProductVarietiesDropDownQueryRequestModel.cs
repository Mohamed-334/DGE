using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Queries.RequestModels
{
    public class GetProductVarietiesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
