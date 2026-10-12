using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.UserPermissions.Commands.RequestModels
{
    public class AddUserPermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
