using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace BaseArchitecture.Core.Features.RolePermissions.Queries.RequestModels
{
    public class GetRolePermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
