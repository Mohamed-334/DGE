using DGE.Core.Features.ContactUsMessages.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ContactUsMessages.Queries.RequestModels
{
    public class GetContactUsMessagesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ContactUsFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetContactUsMessagesPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetContactUsMessagesPaginatedListQueryRequestModel() { }
    }
}
