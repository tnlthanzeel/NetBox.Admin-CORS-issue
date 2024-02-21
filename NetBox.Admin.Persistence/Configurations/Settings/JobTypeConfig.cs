using NetBox.Admin.Core.Settings.JobTypes;

namespace NetBox.Admin.Persistence.Configurations.Settings;

sealed class JobTypeConfig : IEntityTypeConfiguration<JobType>
{
    public void Configure(EntityTypeBuilder<JobType> builder)
    {
        builder.HasQueryFilter(s => s.IsDeleted == false);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(AppConstants.StringLengths.FirstName)
               .Metadata.SetValueComparer(AppDbContextExtensions.IndexedStringComparer); ;

        builder.HasIndex(s => s.Name).HasFilter($"{nameof(JobType.IsDeleted)} <> 1").IsUnique();
    }
}
