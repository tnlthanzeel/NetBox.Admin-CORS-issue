namespace NetBox.Admin.Core.Settings.Services.DTOs;

public sealed record ServiceDto(Guid ServiceTypeId, Guid Id, string Name, decimal Rate);
