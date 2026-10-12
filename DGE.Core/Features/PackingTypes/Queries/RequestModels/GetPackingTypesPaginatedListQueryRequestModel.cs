using DGE.Core.Features.PackingTypes.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Queries.RequestModels
{
    public class GetPackingTypesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<PackingTypeFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetPackingTypesPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetPackingTypesPaginatedListQueryRequestModel() { }
    }
}
