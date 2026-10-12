using DGE.Core.Features.Products.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Queries.RequestModels
{
    public class GetProductsListQueryRequestModel : IRequest<Response<List<ProductFullDataDto>>>
    {
    }
}
