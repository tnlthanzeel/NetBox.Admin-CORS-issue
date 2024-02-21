using NetBox.Admin.Core.Jobs;

namespace NetBox.Admin.Persistence.Configurations.JobConfigs;
sealed class JobConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
               .WithMany()
               .HasForeignKey(k => k.CustomerId);

        builder.Property(x => x.PhonNumber).IsRequired().HasMaxLength(AppConstants.StringLengths.PhoneNumber);
        builder.Property(x => x.TokenNumber).IsRequired().HasMaxLength(64);
        builder.HasIndex(x => new { x.TokenNumber, x.TokenNumberMasterDate }).IsDescending(false, true).IsUnique();

        builder.HasOne(x => x.ClientType)
               .WithMany()
               .HasForeignKey(k => k.ClientTypeId);

        builder.HasOne(x => x.JobType)
               .WithMany()
               .HasForeignKey(k => k.JobTypeId);

        var navigation = builder.Metadata.FindNavigation(nameof(Job.Asignees));
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(s => s.Asignees)
               .WithOne(x => x.Job)
               .HasForeignKey(f => f.JobId);

        builder.HasQueryFilter(q => q.IsDeleted == false);

        navigation = builder.Metadata.FindNavigation(nameof(Job.TimePeriods));
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(s => s.TimePeriods)
               .WithOne(x => x.Job)
               .HasForeignKey(f => f.JobId);

        builder.HasOne(x => x.CurrentAsignee)
               .WithMany()
               .HasForeignKey(k => k.CurrentAsigneeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
