using DGE.Core.Features.ProductMinerals.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Queries.RequestModels
{
    public class GetProductMineralByIdQueryRequestModel : IRequest<Response<ProductMineralFullDataDto>>
    {
        public int Id { get; set; }
    }
}
