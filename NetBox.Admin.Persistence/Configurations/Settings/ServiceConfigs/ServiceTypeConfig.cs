using NetBox.Admin.Core.Settings.Services;

namespace NetBox.Admin.Persistence.Configurations.Settings.ServiceConfigs;

sealed class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasQueryFilter(p => p.IsDeleted == false);

        builder.Property(k => k.Name)
               .IsRequired()
               .HasMaxLength(AppConstants.StringLengths.FirstName)
               .Metadata.SetValueComparer(AppDbContextExtensions.IndexedStringComparer);

        builder.HasMany(s => s.Services)
               .WithOne(s => s.ServiceType)
               .HasForeignKey(f => f.ServiceTypeId);

        var navigation = builder.Metadata.FindNavigation(nameof(ServiceType.Services));
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(s => s.Name).HasFilter($"{nameof(ServiceType.IsDeleted)} <> 1").IsUnique();
    }
}
