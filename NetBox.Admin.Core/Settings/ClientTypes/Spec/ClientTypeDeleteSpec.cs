namespace NetBox.Admin.Core.Settings.ClientTypes.Spec;

internal sealed class ClientTypeDeleteSpec : Specification<ClientType>
{
    public ClientTypeDeleteSpec(Guid clientTypeId)
    {
        Query.Where(e => e.Id == clientTypeId)
             .AsTracking()
             .AsSplitQuery();
    }
}
