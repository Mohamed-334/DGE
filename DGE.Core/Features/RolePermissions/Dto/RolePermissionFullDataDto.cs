namespace DGE.Core.Features.RolePermissions.Dto
{
    public class RolePermissionFullDataDto
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
