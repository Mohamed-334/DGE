using DGE.Core.Features.ProductTypes.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Queries.RequestModels
{
    public class GetProductTypesListQueryRequestModel : IRequest<Response<List<ProductTypeFullDataDto>>>
    {
    }
}
