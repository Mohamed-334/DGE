using DGE.Core.Features.RolePermissions.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.RolePermissions.Queries.RequestModels
{
    public class GetRolePermissionsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<RolePermissionFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetRolePermissionsPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetRolePermissionsPaginatedListQueryRequestModel() { }
    }
}
