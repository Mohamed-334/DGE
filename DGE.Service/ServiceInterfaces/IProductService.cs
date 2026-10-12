using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<PaginatedList<Product>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productCategoryId = null);
        Task<Product?> GetByIdWithIncludesAsync(int id);
        Task<List<Product>> GetAllWithIncludesAsync();
    }
}
