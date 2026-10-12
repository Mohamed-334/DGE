using DGE.Core.Features.ProductImages.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductImages.Queries.RequestModels
{
    public class GetProductImagesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductImageFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public GetProductImagesPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
        }
        public GetProductImagesPaginatedListQueryRequestModel() { }
    }
}
