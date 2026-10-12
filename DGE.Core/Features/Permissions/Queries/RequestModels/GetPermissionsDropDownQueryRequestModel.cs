using BaseArchitecture.Core.Shared.Models;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
