using Microsoft.EntityFrameworkCore.Storage;

namespace NetBox.Admin.Core.Common.Interfaces
{
    public interface IUnitOfWork : IBaseRepository
    {
        Task<IDbContextTransaction> BeginTransaction();
    }
}
