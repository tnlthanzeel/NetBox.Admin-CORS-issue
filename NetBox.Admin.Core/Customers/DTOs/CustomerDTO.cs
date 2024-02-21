namespace NetBox.Admin.Core.Customers.DTOs;

public sealed record CustomerDTO(Guid Id, string PhoneNumber, string? Name);
