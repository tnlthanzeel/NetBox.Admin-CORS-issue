using NetBox.Admin.Core.Jobs;

namespace NetBox.Admin.Persistence.Configurations.JobConfigs;

sealed class JobTimerConfig : IEntityTypeConfiguration<JobTimer>
{
    public void Configure(EntityTypeBuilder<JobTimer> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
