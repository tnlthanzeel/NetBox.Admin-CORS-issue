namespace NetBox.Admin.Core.Settings.ClientTypes.Spec;

internal sealed class ClientTypeUpdateSpec : Specification<ClientType>
{
    public ClientTypeUpdateSpec(Guid id)
    {
        Query.Where(w => w.Id == id)
             .AsTracking();
    }
}
