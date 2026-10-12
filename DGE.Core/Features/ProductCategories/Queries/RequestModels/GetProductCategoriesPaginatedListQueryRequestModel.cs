using DGE.Core.Features.ProductCategories.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Queries.RequestModels
{
    public class GetProductCategoriesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductCategoryFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetProductCategoriesPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetProductCategoriesPaginatedListQueryRequestModel() { }
    }
}
