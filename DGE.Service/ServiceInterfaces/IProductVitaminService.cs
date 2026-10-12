using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductVitaminService : IBaseService<ProductVitamin>
    {
        Task<PaginatedList<ProductVitamin>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? vitaminId = null);
        Task<ProductVitamin?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductVitamin>> GetAllWithIncludesAsync();
    }
}
