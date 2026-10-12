using DGE.Core.Features.ProductHarvestMonths.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels
{
    public class GetProductHarvestMonthByIdQueryRequestModel : IRequest<Response<ProductHarvestMonthFullDataDto>>
    {
        public int Id { get; set; }
    }
}
