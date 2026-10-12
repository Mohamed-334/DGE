namespace DGE.Core.Features.UserPermissions.Dto
{
    public class UserPermissionFullDataDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
    }
}
