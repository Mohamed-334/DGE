namespace DGE.Service.Shared.Interface
{
    public interface IBaseService<TEntity> :
        IReadService<TEntity>,
        IWriteService<TEntity>,
        IDeleteService<TEntity>,
        ITransactionalService
        where TEntity : class
    {
    }
}
