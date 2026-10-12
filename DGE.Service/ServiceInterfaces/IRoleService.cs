using DGE.Domain.Entities;
using DGE.Service.Shared.PaginatedList;
using Microsoft.AspNetCore.Identity;

namespace DGE.Service.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<List<Role>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Role?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default);
        Task<IdentityResult> AddRoleAsync(Role role);
        Task<IdentityResult> EditAsync(Role entity);
        Task<IdentityResult> HardDeleteAsync(Role entity);
        Task<PaginatedList<Role>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<bool> IsRoleNameExistAsync(string email);
        Task<bool> RoleIsUsedAsync(int roleId, CancellationToken cancellationToken = default);
        Task<List<Role>> GetUserRolesAsync(int UserId, CancellationToken cancellationToken = default);
    }
}
