using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.ExtensionMethods;
using DGE.Service.Shared.PaginatedList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Linq.Expressions;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Service.Service
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        #endregion

        #region Constructor
        public UserService(UserManager<User> userManager, IStringLocalizer<AppLocalization> stringLocalizer, IFileService fileService)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _fileService = fileService;
        }
        #endregion

        #region Read Projection
        // Read-only shape of a user: one SQL query (users joined to their roles) that selects only the
        // columns the read endpoints map, so password hashes and security stamps never leave the database
        private static readonly Expression<Func<User, User>> ReadProjection = u => new User
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Name = u.Name,
            NameLocalization = u.NameLocalization,
            NationalNumber = u.NationalNumber,
            Address = u.Address,
            ProfileImage = u.ProfileImage,
            IsDeleted = u.IsDeleted,
            CreatorName = u.CreatorName,
            CreationDate = u.CreationDate,
            ModifierName = u.ModifierName,
            ModificationDate = u.ModificationDate,
            UserRoles = u.UserRoles!.Select(ur => new UserRole
            {
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                Role = new Role { Id = ur.Role!.Id, Name = ur.Role.Name, NameLocalization = ur.Role.NameLocalization }
            }).ToList()
        };
        #endregion

        #region Methods
        public async Task<List<User>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Users = await _userManager.Users
                            .AsNoTracking()
                            .Where(x => x.IsDeleted == false)
                            .Select(ReadProjection)
                            .ToListAsync(cancellationToken);
            return Users;
        }
        public async Task<User?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default)
        {
            var User = await _userManager.Users
                            .AsNoTracking()
                            .Where(u => u.Id == id)
                            .Select(ReadProjection)
                            .FirstOrDefaultAsync(cancellationToken);
            return User;
        }
        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var User = await _userManager.Users
                            .Include(u => u.UserRoles!)
                            .ThenInclude(ur => ur.Role)
                            .Where(u => u.Id == id)
                            .FirstOrDefaultAsync(cancellationToken);
            return User;
        }
        public async Task<IdentityResult> EditAsync(User entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result;
        }
        public async Task<IdentityResult> HardDeleteAsync(User entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            return result;
        }
        public async Task<PaginatedList<User>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var Users = _userManager.Users
                                    .AsNoTracking()
                                    .Where(x => x.IsDeleted == false)
                                    .OrderBy(x => x.Id)
                                    .Select(ReadProjection);

            var PaginatedList = await Users.ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
            return PaginatedList;
        }
        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }
        public async Task<IdentityResult> AddUserToRoleAsync(User user, string role) => await _userManager.AddToRoleAsync(user, role);
        public async Task<IdentityResult> RemoveRolesAsync(User user, List<string> roles) => await _userManager.RemoveFromRolesAsync(user, roles);
        public async Task<bool> IsUserInRoleAsync(User user, string role) => (role != null && await _userManager.IsInRoleAsync(user, role));
        public async Task<User?> GetUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
        public async Task<bool> IsUserNameExistAsync(string userName) => (await _userManager.FindByNameAsync(userName)) != null;
        public async Task<bool> IsUserIdExistAsync(int Id) => (await _userManager.FindByIdAsync(Id.ToString())) != null;
        public async Task<bool> IsEmailExistAsync(string email) => (await _userManager.FindByEmailAsync(email)) != null;

        public async Task<string?> UploadFileAsync(string FolderName, IFormFile file)
        {
            var UploadResult = await _fileService.UploadFileAsync(FolderName, file, FileCategoryEnum.Image);
            if (UploadResult == _stringLocalizer[AppLocalizationKeys.FailedToUploadImage])
                return _stringLocalizer[AppLocalizationKeys.FailedToUploadImage];

            if (UploadResult == _stringLocalizer[AppLocalizationKeys.NoImage])
                return null;
            else
                return UploadResult;
        }
        #endregion
    }


}
