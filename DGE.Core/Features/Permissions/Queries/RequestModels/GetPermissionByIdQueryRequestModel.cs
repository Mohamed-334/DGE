using BaseArchitecture.Core.Features.Permissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionByIdQueryRequestModel : IRequest<Response<PermissionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
