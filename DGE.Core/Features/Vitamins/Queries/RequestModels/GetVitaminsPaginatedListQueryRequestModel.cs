using DGE.Core.Features.Vitamins.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Vitamins.Queries.RequestModels
{
    public class GetVitaminsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<VitaminFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetVitaminsPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetVitaminsPaginatedListQueryRequestModel() { }
    }
}
