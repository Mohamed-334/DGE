using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
