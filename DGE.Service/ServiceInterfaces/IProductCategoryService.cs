using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductCategoryService : IBaseService<ProductCategory>
    {
        Task<PaginatedList<ProductCategory>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
    }
}
