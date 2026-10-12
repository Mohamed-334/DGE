using DGE.Core.Features.ProductVitamins.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Queries.RequestModels
{
    public class GetProductVitaminsListQueryRequestModel : IRequest<Response<List<ProductVitaminFullDataDto>>>
    {
    }
}
