
using DGE.Core.Features.ApplicationUser.DTO;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ApplicationUser.Queries.RequestModels
{
    public class GetUsersPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<UserFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetUsersPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetUsersPaginatedListQueryRequestModel() { }
    }
}
