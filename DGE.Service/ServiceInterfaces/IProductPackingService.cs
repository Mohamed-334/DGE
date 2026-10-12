using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductPackingService : IBaseService<ProductPacking>
    {
        Task<PaginatedList<ProductPacking>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? packingTypeId = null);
        Task<ProductPacking?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductPacking>> GetAllWithIncludesAsync();
    }
}
