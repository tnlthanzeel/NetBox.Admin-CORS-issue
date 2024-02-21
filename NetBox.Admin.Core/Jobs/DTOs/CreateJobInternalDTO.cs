namespace NetBox.Admin.Core.Jobs.DTOs;

sealed record CreateJobInternalDTO(Guid CustomerId,
                                   string PhoneNumber,
                                   DateOnly TokenMasterDate,
                                   string TokenNumber,
                                   Guid ClientTypeId,
                                   Guid DesignerId,
                                   Guid DesignSentByModeId,
                                   Guid JobTypeId);
