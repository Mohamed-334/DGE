using System.Linq.Expressions;

namespace DGE.Service.Shared.Interface
{
    public interface IDeleteService<TEntity> where TEntity : class
    {
        Task<string> HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<string> HardDeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<string> SoftDeleteAndActivationAsync(int id, CancellationToken cancellationToken = default);
    }
}
