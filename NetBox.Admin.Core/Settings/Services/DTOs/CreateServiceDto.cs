namespace NetBox.Admin.Core.Settings.Services.DTOs;

public sealed record CreateServiceDto(string Name, decimal Rate = 0M);
