using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class UserPermission : BaseEntity
    {
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? PermissionId { get; set; }
        public virtual Permission? Permission { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
