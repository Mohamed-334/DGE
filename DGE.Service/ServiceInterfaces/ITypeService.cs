using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Service.ServiceInterfaces
{
    public interface ITypeService : IBaseService<Type>
    {
        Task<PaginatedList<Type>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
        Task<List<Type>> GetByNamesAsync(IEnumerable<string> names);
    }
}
