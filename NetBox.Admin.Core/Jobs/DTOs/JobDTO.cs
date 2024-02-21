namespace NetBox.Admin.Core.Jobs.DTOs;

public sealed record JobDTO(Guid JobId, Guid DesignerId, string TokenNumber, DateOnly TokenDate);
