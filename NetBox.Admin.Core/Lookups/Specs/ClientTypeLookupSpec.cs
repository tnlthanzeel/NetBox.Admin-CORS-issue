using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Settings.ClientTypes;

namespace NetBox.Admin.Core.Lookups.Specs;

sealed class ClientTypeLookupSpec : Specification<ClientType, ClientTypeLookupDTO>
{
    public ClientTypeLookupSpec()
    {
        Query.OrderBy(o => o.ClientTypeValue);

        Query.Select(s => new ClientTypeLookupDTO(s.Id, s.ClientTypeValue));
    }
}
