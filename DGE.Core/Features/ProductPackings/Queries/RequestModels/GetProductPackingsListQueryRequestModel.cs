using DGE.Core.Features.ProductPackings.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Queries.RequestModels
{
    public class GetProductPackingsListQueryRequestModel : IRequest<Response<List<ProductPackingFullDataDto>>>
    {
    }
}
