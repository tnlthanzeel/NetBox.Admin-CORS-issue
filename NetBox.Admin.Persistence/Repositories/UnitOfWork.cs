using Microsoft.EntityFrameworkCore.Storage;
using NetBox.Admin.Core.Common.Interfaces;

namespace NetBox.Admin.Persistence.Repositories;

sealed class UnitOfWork : BaseRepository, IUnitOfWork
{
    public UnitOfWork(AppDbContext dbContext) : base(dbContext) { }

    public Task<IDbContextTransaction> BeginTransaction()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }
}
