using DGE.Core.Features.ProductVitamins.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Queries.RequestModels
{
    public class GetProductVitaminsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductVitaminFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public int? VitaminId { get; set; }
        public GetProductVitaminsPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId, int? vitaminId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
            VitaminId = vitaminId;
        }
        public GetProductVitaminsPaginatedListQueryRequestModel() { }
    }
}
