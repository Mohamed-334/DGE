using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Domain.Entities
{
    public class Permission : BaseEntityWithName
    {
        public int? PermissionNumber { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
