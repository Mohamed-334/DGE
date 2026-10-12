using DGE.Core.Features.Roles.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Roles.Queries.RequestModels
{
    public class GetRolesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<RoleFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetRolesPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetRolesPaginatedListQueryRequestModel() { }
    }
}
