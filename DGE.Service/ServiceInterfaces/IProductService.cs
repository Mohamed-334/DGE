using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<PaginatedList<Product>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productCategoryId = null, bool? showOnHome = null);
        Task<Product?> GetByIdWithIncludesAsync(int id);
        Task<Product?> GetBySlugWithIncludesAsync(string slug);
        Task<List<Product>> GetAllWithIncludesAsync();
        Task<bool> IsSlugExistAsync(string slug, int? excludeProductId = null);
        Task<string> GenerateUniqueSlugAsync(string? name, int? excludeProductId = null);
    }
}
