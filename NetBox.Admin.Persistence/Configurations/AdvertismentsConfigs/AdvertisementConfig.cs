using NetBox.Admin.Core.Advertisements;

namespace NetBox.Admin.Persistence.Configurations.AdvertismentsConfigs;

internal sealed class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasKey(x => x.Id);
        builder.Property(p => p.FilelURL).IsRequired().HasMaxLength(AppConstants.StringLengths.Description);
    }
}
