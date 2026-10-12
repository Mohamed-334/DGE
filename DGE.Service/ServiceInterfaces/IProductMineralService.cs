using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductMineralService : IBaseService<ProductMineral>
    {
        Task<PaginatedList<ProductMineral>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? mineralId = null);
        Task<ProductMineral?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductMineral>> GetAllWithIncludesAsync();
    }
}
