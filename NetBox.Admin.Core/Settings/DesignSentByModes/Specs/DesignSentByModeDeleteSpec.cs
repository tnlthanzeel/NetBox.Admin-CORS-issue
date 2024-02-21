namespace NetBox.Admin.Core.Settings.DesignSentByModes.Specs;

internal sealed class DesignSentByModeDeleteSpec : Specification<DesignSentByMode>
{
    public DesignSentByModeDeleteSpec(Guid id)
    {
        Query.Where(w => w.Id == id)
             .AsTracking();
    }
}
