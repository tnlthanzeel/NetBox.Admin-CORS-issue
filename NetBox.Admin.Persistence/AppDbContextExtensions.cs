using NetBox.Admin.SharedKernal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NetBox.Admin.Persistence;

static class AppDbContextExtensions
{
    internal static ValueComparer<string> IndexedStringComparer = new ValueComparer<string>((l, r) => string.Equals(l, r, StringComparison.Ordinal),
                                                                                            v => v.GetHashCode(),
                                                                                            v => v);

    internal static void ApplyCommanConfigurations(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ICreatedAudit).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).Property<string>(nameof(ICreatedAudit.CreatedBy)).HasMaxLength(250);
            }

            if (typeof(IUpdatedAudit).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).Property<string>(nameof(IUpdatedAudit.UpdatedBy)).HasMaxLength(250);
            }

            if (typeof(IDeletedAudit).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).Property<string>(nameof(IDeletedAudit.DeletedBy)).HasMaxLength(250);

                builder.Entity(entityType.ClrType).HasIndex(nameof(IDeletedAudit.IsDeleted));
            }
        }
    }
}