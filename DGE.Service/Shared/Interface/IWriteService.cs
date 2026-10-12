namespace DGE.Service.Shared.Interface
{
    public interface IWriteService<TEntity> where TEntity : class
    {
        Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<string> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        Task<string> EditAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<string> EditRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
