using DGE.Core.Features.Products.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Queries.RequestModels
{
    public class GetProductBySlugQueryRequestModel : IRequest<Response<ProductFullDataDto>>
    {
        public string Slug { get; set; } = string.Empty;
    }
}
