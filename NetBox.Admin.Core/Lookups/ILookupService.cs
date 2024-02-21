using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Lookups.Filters;

namespace NetBox.Admin.Core.Lookups;

public interface ILookupService
{
    Task<ResponseResult<IReadOnlyList<ClientTypeLookupDTO>>> GetClientTypesForLookup(CancellationToken cancellationToken);
    Task<ResponseResult<IReadOnlyList<CustomerLookupDTO>>> GetCustomersForLookup(CustomerLookupFilter filter, CancellationToken cancellationToken);
    Task<ResponseResult<IReadOnlyList<DesignSentByModeLookupDTO>>> GetDesignSentByModes(CancellationToken token);
    Task<ResponseResult<IReadOnlyList<JobTypeLookupDTO>>> GetJobTypesForLookup(CancellationToken token);
    Task<ResponseResult<IReadOnlyList<UserLookupDTO>>> GetUsersForLookup(CancellationToken token);
}
