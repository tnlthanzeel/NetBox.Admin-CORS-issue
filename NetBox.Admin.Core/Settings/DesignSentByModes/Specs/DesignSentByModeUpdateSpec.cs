namespace NetBox.Admin.Core.Settings.DesignSentByModes.Specs;

internal sealed class DesignSentByModeUpdateSpec : Specification<DesignSentByMode>
{
    public DesignSentByModeUpdateSpec(Guid id)
    {
        Query.Where(w => w.Id == id)
             .AsTracking();
    }
}
