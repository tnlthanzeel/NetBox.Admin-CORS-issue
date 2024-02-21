using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Specs;

internal sealed class DesignSentByModeByIdSpec : Specification<DesignSentByMode, DesignSentByModeDto>
{
    public DesignSentByModeByIdSpec(Guid id)
    {
        Query.Where(w => w.Id == id);

        Query.Select(e => new DesignSentByModeDto(e.Id, e.Mode, e.ImageURL));
    }
}
