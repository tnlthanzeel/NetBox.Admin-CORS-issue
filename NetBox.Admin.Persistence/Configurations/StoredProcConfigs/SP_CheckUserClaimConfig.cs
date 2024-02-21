using NetBox.Admin.Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetBox.Admin.Persistence.Configurations.StoredProcConfigs;

internal sealed class SP_CheckUserClaimConfig : IEntityTypeConfiguration<SP_CheckUserClaim>
{
    public void Configure(EntityTypeBuilder<SP_CheckUserClaim> builder)
    {
        builder.HasNoKey();
        builder.ToTable((string)null!);
    }
}
