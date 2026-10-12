using Microsoft.AspNetCore.Http;
using static BaseArchitecture.Domain.Enums.EnumExtensions;

namespace BaseArchitecture.Service.ServiceInterfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(string DirectorName, IFormFile file, FileCategoryEnum allowedCategories);
    }
}
