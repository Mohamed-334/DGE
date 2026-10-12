using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductTypeService : IBaseService<ProductType>
    {
        Task<PaginatedList<ProductType>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? typeId = null);
        Task<ProductType?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductType>> GetAllWithIncludesAsync();
    }
}
