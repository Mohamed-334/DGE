using DGE.Service.Shared.PaginatedList;
using System.Linq.Expressions;

namespace DGE.Service.Shared.Interface
{
    public interface IReadService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
        Task<PaginatedList<TEntity>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
