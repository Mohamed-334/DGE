using DGE.Core.Features.ProductVarieties.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Queries.RequestModels
{
    public class GetProductVarietiesListQueryRequestModel : IRequest<Response<List<ProductVarietyFullDataDto>>>
    {
    }
}
