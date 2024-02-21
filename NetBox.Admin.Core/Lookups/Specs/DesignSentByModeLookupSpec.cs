using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Settings.DesignSentByModes;

namespace NetBox.Admin.Core.Lookups.Specs;

sealed class DesignSentByModeLookupSpec : Specification<DesignSentByMode, DesignSentByModeLookupDTO>
{
    public DesignSentByModeLookupSpec()
    {
        Query.OrderBy(o => o.Mode);

        Query.Select(s => new DesignSentByModeLookupDTO(s.Id, s.Mode, s.ImageURL));
    }
}
