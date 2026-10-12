using DGE.Core.Features.UserPermissions.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<UserPermissionFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetUserPermissionsPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetUserPermissionsPaginatedListQueryRequestModel() { }
    }
}
