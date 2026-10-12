using DGE.Core.Features.ProductHarvestMonths.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels
{
    public class GetProductHarvestMonthsListQueryRequestModel : IRequest<Response<List<ProductHarvestMonthFullDataDto>>>
    {
    }
}
