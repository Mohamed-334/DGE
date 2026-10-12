using DGE.Core.Features.Permissions.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<PermissionFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetPermissionsPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetPermissionsPaginatedListQueryRequestModel() { }
    }
}
