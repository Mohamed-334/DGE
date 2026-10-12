using Microsoft.EntityFrameworkCore.Storage;

namespace BaseArchitecture.Service.Shared.Interface
{
    public interface ITransactionalService
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
