using DGE.Domain.Entities;
using DGE.Service.Shared.PaginatedList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DGE.Service.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<User>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<IdentityResult> AddUserToRoleAsync(User user, string role);
        Task<IdentityResult> RemoveRolesAsync(User user, List<string> roles);
        Task<bool> IsUserInRoleAsync(User user, string role);
        Task<IdentityResult> EditAsync(User entity);
        Task<IdentityResult> HardDeleteAsync(User entity);
        Task<PaginatedList<User>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsUserIdExistAsync(int Id);
        Task<bool> IsEmailExistAsync(string email);
        Task<string?> UploadFileAsync(string FolderName, IFormFile file);
    }
}
