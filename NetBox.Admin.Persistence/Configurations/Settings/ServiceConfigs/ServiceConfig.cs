using NetBox.Admin.Core.Settings.Services;
using NetBox.Admin.Persistence.Utilities;

namespace NetBox.Admin.Persistence.Configurations.Settings.ServiceConfigs;

sealed class ServiceConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasQueryFilter(p => p.IsDeleted == false);
        builder.Property(k => k.Name)
               .IsRequired()
               .HasMaxLength(AppConstants.StringLengths.FirstName)
               .Metadata.SetValueComparer(AppDbContextExtensions.IndexedStringComparer);

        builder.HasIndex(s => s.Name)
               .HasFilter($"{nameof(Service.IsDeleted)} <> 1")
               .IsUnique();
        builder.Property(s => s.Rate).DecimalPrecision();
    }
}
