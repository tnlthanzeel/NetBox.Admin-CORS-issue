namespace NetBox.Admin.Core.Settings.Services.DTOs;

public sealed record UpdateServiceDto(string Name, decimal Rate = 0M);
