using NetBox.Admin.Core.Advertisements.DTOs;

namespace NetBox.Admin.Core.Advertisements.Specs;

sealed class AdvertisementListSpec : Specification<Advertisement, AdvertisementDto>
{
    public AdvertisementListSpec()
    {
        Query.OrderByDescending(w => w.CreatedOn);

        Query.Select(e => new AdvertisementDto(e.Id, e.FilelURL, e.IsImage));
    }
}
