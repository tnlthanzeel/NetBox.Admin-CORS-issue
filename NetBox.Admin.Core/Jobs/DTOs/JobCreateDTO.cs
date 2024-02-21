namespace NetBox.Admin.Core.Jobs.DTOs;

public sealed record JobCreateDTO(Guid? CustomerId,
                                  string? PhoneNumber,
                                  string? CustomerName,
                                  Guid DesignerId,
                                  Guid ClientType,
                                  Guid JobType,
                                  Guid DesignSentByModeId);
