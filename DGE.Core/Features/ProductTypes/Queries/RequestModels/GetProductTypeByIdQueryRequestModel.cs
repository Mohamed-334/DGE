using DGE.Core.Features.ProductTypes.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Queries.RequestModels
{
    public class GetProductTypeByIdQueryRequestModel : IRequest<Response<ProductTypeFullDataDto>>
    {
        public int Id { get; set; }
    }
}
