using BaseArchitecture.Core.Features.UserPermissions.Dto;
using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionByIdQueryRequestModel : IRequest<Response<UserPermissionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
