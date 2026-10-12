using DGE.Core.Features.Minerals.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Minerals.Queries.RequestModels
{
    public class GetMineralsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<MineralFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetMineralsPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetMineralsPaginatedListQueryRequestModel() { }
    }
}
