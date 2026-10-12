using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.UserPermissions.Commands.RequestModels
{
    public class UpdateUserPermissionCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
