using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface IPackingTypeService : IBaseService<PackingType>
    {
        Task<PaginatedList<PackingType>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
        Task<List<PackingType>> GetByNamesAsync(IEnumerable<string> names);
    }
}
