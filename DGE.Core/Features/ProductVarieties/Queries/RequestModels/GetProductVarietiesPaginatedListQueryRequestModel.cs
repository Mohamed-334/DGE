using DGE.Core.Features.ProductVarieties.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Queries.RequestModels
{
    public class GetProductVarietiesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductVarietyFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public int? ProductId { get; set; }
        public GetProductVarietiesPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search, int? productId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
            ProductId = productId;
        }
        public GetProductVarietiesPaginatedListQueryRequestModel() { }
    }
}
