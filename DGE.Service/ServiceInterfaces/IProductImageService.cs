using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IProductImageService : IBaseService<ProductImage>
    {
        Task<PaginatedList<ProductImage>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null);
        Task<ProductImage?> GetByIdWithIncludesAsync(int id);
        Task<List<ProductImage>> GetAllWithIncludesAsync();
    }
}
