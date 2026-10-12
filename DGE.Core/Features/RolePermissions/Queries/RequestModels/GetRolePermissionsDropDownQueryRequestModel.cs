using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.RolePermissions.Queries.RequestModels
{
    public class GetRolePermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
