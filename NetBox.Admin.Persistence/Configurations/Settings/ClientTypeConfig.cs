using NetBox.Admin.Core.Settings.ClientTypes;

namespace NetBox.Admin.Persistence.Configurations.Settings;

sealed class ClientTypeConfig : IEntityTypeConfiguration<ClientType>
{
    public void Configure(EntityTypeBuilder<ClientType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClientTypeValue)
               .IsRequired()
               .HasMaxLength(AppConstants.StringLengths.FirstName)
               .Metadata.SetValueComparer(AppDbContextExtensions.IndexedStringComparer);

        builder.HasIndex(s => s.ClientTypeValue)
               .HasFilter($"{nameof(ClientType.IsDeleted)} <> 1")
               .IsUnique();

        builder.HasQueryFilter(s => s.IsDeleted == false);
    }
}
