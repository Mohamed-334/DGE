using DGE.Core.Features.ProductImages.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductImages.Queries.RequestModels
{
    public class GetProductImageByIdQueryRequestModel : IRequest<Response<ProductImageFullDataDto>>
    {
        public int Id { get; set; }
    }
}
