using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductHarvestMonthService : IBaseService<ProductHarvestMonth>
    {
        Task<PaginatedList<ProductHarvestMonth>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? monthStatusCode = null);
        Task<ProductHarvestMonth?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductHarvestMonth>> GetAllWithIncludesAsync();
    }
}
