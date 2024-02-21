using NetBox.Admin.Core.Settings.DesignSentByModes;

namespace NetBox.Admin.Persistence.Configurations.Settings;

sealed class DesignSentByModeConfig : IEntityTypeConfiguration<DesignSentByMode>
{
    public void Configure(EntityTypeBuilder<DesignSentByMode> builder)
    {
        builder.HasQueryFilter(s => s.IsDeleted == false);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Mode)
               .IsRequired()
               .HasMaxLength(AppConstants.StringLengths.FirstName)
               .Metadata.SetValueComparer(AppDbContextExtensions.IndexedStringComparer);

        builder.HasIndex(s => s.Mode).HasFilter($"{nameof(DesignSentByMode.IsDeleted)} <> 1").IsUnique();

        builder.Property(s => s.ImageURL).IsRequired().HasMaxLength(AppConstants.StringLengths.Description);
    }
}
