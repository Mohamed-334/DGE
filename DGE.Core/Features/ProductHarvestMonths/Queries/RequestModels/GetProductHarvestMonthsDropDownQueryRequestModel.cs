using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels
{
    public class GetProductHarvestMonthsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
