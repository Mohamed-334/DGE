using BaseArchitecture.Core.Features.RolePermissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Service.Shared.PaginatedList;
using MediatR;

namespace BaseArchitecture.Core.Features.RolePermissions.Queries.RequestModels
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
