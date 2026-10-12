using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Permissions.Queries.RequestModels
{
    public class GetPermissionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
