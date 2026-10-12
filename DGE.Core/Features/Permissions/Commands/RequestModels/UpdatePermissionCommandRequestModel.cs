using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Commands.RequestModels
{
    public class UpdatePermissionCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public int PermissionNumber { get; set; }
    }
}
