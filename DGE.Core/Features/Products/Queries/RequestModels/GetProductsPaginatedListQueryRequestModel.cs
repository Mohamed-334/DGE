using DGE.Core.Features.Products.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Products.Queries.RequestModels
{
    public class GetProductsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public int? ProductCategoryId { get; set; }
        public GetProductsPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search, int? productCategoryId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
            ProductCategoryId = productCategoryId;
        }
        public GetProductsPaginatedListQueryRequestModel() { }
    }
}
