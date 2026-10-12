using DGE.Core.Features.ProductMinerals.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Queries.RequestModels
{
    public class GetProductMineralsListQueryRequestModel : IRequest<Response<List<ProductMineralFullDataDto>>>
    {
    }
}
