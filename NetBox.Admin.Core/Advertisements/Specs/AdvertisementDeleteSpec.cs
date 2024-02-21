using NetBox.Admin.Core.Advertisements;

namespace NetBox.Admin.Core.Advertisements.Specs;

internal sealed class AdvertisementDeleteSpec : Specification<Advertisement>
{
    public AdvertisementDeleteSpec(Guid id) => Query.AsTracking()
                                                    .Where(e => e.Id == id);
}
