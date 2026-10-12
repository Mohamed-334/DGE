using DGE.Core.Features.ProductImages.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductImages.Queries.RequestModels
{
    public class GetProductImagesListQueryRequestModel : IRequest<Response<List<ProductImageFullDataDto>>>
    {
    }
}
