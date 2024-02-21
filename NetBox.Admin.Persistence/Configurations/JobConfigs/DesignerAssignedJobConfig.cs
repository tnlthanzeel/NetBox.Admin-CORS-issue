using NetBox.Admin.Core.Jobs;

namespace NetBox.Admin.Persistence.Configurations.JobConfigs;

sealed class DesignerAssignedJobConfig : IEntityTypeConfiguration<DesignerAssignedJob>
{
    public void Configure(EntityTypeBuilder<DesignerAssignedJob> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(opa => opa.Asignee)
               .WithMany()
               .HasForeignKey(opa => opa.AsigneeId);

        builder.HasQueryFilter(q => q.IsDeleted == false);
    }
}
