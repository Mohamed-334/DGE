using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Queries.RequestModels
{
    public class GetProductTypesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
