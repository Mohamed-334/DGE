using DGE.Core.Features.Types.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Types.Queries.RequestModels
{
    public class GetTypesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<TypeFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetTypesPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetTypesPaginatedListQueryRequestModel() { }
    }
}
