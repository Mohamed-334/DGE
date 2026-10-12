using Microsoft.AspNetCore.Http;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Service.ServiceInterfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(string DirectorName, IFormFile file, FileCategoryEnum allowedCategories);
    }
}
