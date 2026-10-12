using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductVarietyService : IBaseService<ProductVariety>
    {
        Task<PaginatedList<ProductVariety>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productId = null);
        Task<ProductVariety?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductVariety>> GetAllWithIncludesAsync();
    }
}
