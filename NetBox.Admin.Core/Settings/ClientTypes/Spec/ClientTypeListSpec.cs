using NetBox.Admin.Core.Settings.ClientTypes.DTOs;

namespace NetBox.Admin.Core.Settings.ClientTypes.Spec;

internal sealed class ClientTypeListSpec : Specification<ClientType, ClientTypeDto>
{
    public ClientTypeListSpec()
    {
        Query.OrderBy(w => w.ClientTypeValue);

        Query.Select(e => new ClientTypeDto(e.Id, e.ClientTypeValue));
    }
}
