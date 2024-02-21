namespace NetBox.Admin.Core.Common.Interfaces;

public interface IBaseRepository
{
    Task<int> SaveChangesAsync(CancellationToken token = new());
}
