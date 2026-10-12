using DGE.Core.Features.ProductVarieties.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Queries.RequestModels
{
    public class GetProductVarietyByIdQueryRequestModel : IRequest<Response<ProductVarietyFullDataDto>>
    {
        public int Id { get; set; }
    }
}
