using DGE.Core.Features.Certifications.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Certifications.Queries.RequestModels
{
    public class GetCertificationsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<CertificationFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public GetCertificationsPaginatedListQueryRequestModel(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
        public GetCertificationsPaginatedListQueryRequestModel() { }
    }
}
