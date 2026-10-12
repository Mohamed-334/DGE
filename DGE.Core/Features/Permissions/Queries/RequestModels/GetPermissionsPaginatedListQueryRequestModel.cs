using BaseArchitecture.Core.Features.Permissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Service.Shared.PaginatedList;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Queries.RequestModels
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
