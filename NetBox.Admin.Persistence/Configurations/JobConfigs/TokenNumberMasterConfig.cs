using NetBox.Admin.Core.Jobs;

namespace NetBox.Admin.Persistence.Configurations.JobConfigs;

sealed class TokenNumberMasterConfig : IEntityTypeConfiguration<TokenNumberMaster>
{
    public void Configure(EntityTypeBuilder<TokenNumberMaster> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Date).IsDescending(true).IsUnique();
        builder.Property(x => x.TokenNumber).IsRequired().HasMaxLength(64);
    }
}
