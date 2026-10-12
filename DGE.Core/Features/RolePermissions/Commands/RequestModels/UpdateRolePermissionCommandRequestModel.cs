using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.RolePermissions.Commands.RequestModels
{
    public class UpdateRolePermissionCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
