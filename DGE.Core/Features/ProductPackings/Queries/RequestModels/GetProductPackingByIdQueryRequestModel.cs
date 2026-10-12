using DGE.Core.Features.ProductPackings.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Queries.RequestModels
{
    public class GetProductPackingByIdQueryRequestModel : IRequest<Response<ProductPackingFullDataDto>>
    {
        public int Id { get; set; }
    }
}
