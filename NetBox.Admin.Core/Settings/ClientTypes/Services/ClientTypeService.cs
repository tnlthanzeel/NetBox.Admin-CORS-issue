using NetBox.Admin.Core.Settings.ClientTypes.DTOs;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;
using NetBox.Admin.Core.Settings.ClientTypes.Spec;
using NetBox.Admin.Core.Settings.ClientTypes.Validators;

namespace NetBox.Admin.Core.Settings.ClientTypes.Services;

internal sealed class ClientTypeService(IClientTypeRepository _clientTypeRepository,
                                        IModelValidator _validator) : IClientTypeService
{
    private readonly IClientTypeRepository _clientTypeRepository = _clientTypeRepository;
    private readonly IModelValidator _validator = _validator;

    public async Task<ResponseResult<ClientTypeDto>> CreateClientType(CreateClientTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateClientTypeDtoValidator, CreateClientTypeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        ClientType clientType = new(model.ClientType);

        _clientTypeRepository.Add(clientType);

        await _clientTypeRepository.SaveChangesAsync();

        ClientTypeDto clientTypeDto = new(clientType.Id, clientType.ClientTypeValue);
        return new(clientTypeDto);
    }

    public async Task<ResponseResult> DeleteClientType(Guid id)
    {
        var clientType = await _clientTypeRepository.GetBySpec(new ClientTypeDeleteSpec(id));

        if (clientType is null) return new(new NotFoundException(nameof(id), "Client Type", id));

        // TODO: validate if any client type in use before deleting
        clientType.Delete();

        await _clientTypeRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<IReadOnlyList<ClientTypeDto>>> GetAllClientTypes(Paginator paginator, CancellationToken token)
    {
        var (list, totalRecords) = await _clientTypeRepository.GetProjectedListBySpec(paginator, new ClientTypeListSpec(), token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult<ClientTypeDto>> GetClientTypeById(Guid id, CancellationToken token)
    {
        var clientTypeDto = await _clientTypeRepository.GetProjectedEntityBySpec(new ClientTypeByIdSpec(id), token);

        if (clientTypeDto is null) return new(new NotFoundException(nameof(id), "Client Type", id));

        return new(clientTypeDto);
    }

    public async Task<ResponseResult> Update(Guid id, UpdateClientTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<UpdateClientTypeDtoValidator, UpdateClientTypeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var clientType = await _clientTypeRepository.GetBySpec(new ClientTypeUpdateSpec(id));

        if (clientType is null) return new(new NotFoundException(nameof(id), "Client Type", id));

        clientType.Update(model.ClientType);

        await _clientTypeRepository.SaveChangesAsync();

        return new();
    }
}
