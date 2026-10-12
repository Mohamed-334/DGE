using Microsoft.EntityFrameworkCore.Storage;

namespace DGE.Service.Shared.Interface
{
    public interface ITransactionalService
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
