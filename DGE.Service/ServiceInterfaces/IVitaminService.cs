using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IVitaminService : IBaseService<Vitamin>
    {
        Task<PaginatedList<Vitamin>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
        Task<bool> AllExistAsync(IEnumerable<int> ids);
    }
}
