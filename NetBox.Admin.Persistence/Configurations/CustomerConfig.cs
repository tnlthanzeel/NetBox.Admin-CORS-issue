using NetBox.Admin.Core.Customers;

namespace NetBox.Admin.Persistence.Configurations;

sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasQueryFilter(w => w.IsDeleted == false);
        builder.HasKey(k => k.Id);
        builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(AppConstants.StringLengths.PhoneNumber);
        builder.Property(p => p.Name).IsRequired(false).HasMaxLength(AppConstants.StringLengths.FirstName);

        builder.HasIndex(p => p.PhoneNumber).IsUnique();
        builder.HasIndex(p => p.Name);
    }
}
