using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace BaseArchitecture.Core.Features.UserPermissions.Queries.RequestModels
{
    public class GetUserPermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
