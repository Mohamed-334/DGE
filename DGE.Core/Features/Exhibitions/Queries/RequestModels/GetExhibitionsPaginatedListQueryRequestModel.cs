using DGE.Core.Features.Exhibitions.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Exhibitions.Queries.RequestModels
{
    public class GetExhibitionsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ExhibitionFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetExhibitionsPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetExhibitionsPaginatedListQueryRequestModel() { }
    }
}
