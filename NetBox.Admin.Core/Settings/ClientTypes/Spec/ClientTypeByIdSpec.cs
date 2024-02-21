using NetBox.Admin.Core.Settings.ClientTypes.DTOs;

namespace NetBox.Admin.Core.Settings.ClientTypes.Spec;

internal sealed class ClientTypeByIdSpec : Specification<ClientType, ClientTypeDto>
{
    public ClientTypeByIdSpec(Guid clientTypeId)
    {
        Query.Where(w => w.Id == clientTypeId);

        Query.Select(e => new ClientTypeDto(e.Id, e.ClientTypeValue));
    }
}
