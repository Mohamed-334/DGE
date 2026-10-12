using DGE.Core.Features.ProductVitamins.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Queries.RequestModels
{
    public class GetProductVitaminByIdQueryRequestModel : IRequest<Response<ProductVitaminFullDataDto>>
    {
        public int Id { get; set; }
    }
}
