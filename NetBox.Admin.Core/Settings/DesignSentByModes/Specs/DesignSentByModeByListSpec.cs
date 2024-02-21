using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Specs;

internal sealed class DesignSentByModeByListSpec : Specification<DesignSentByMode, DesignSentByModeDto>
{
    public DesignSentByModeByListSpec()
    {
        Query.OrderBy(w => w.Mode);

        Query.Select(e => new DesignSentByModeDto(e.Id, e.Mode, e.ImageURL));
    }
}
