using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Filters;
using NetBox.Admin.Core.Settings.Services.Interfaces;
using NetBox.Admin.Core.Settings.Services.Specs;
using NetBox.Admin.Core.Settings.Services.Validators;

namespace NetBox.Admin.Core.Settings.Services.Services;

sealed class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly IModelValidator _validator;

    public ServiceTypeService(IServiceTypeRepository serviceTypeRepository,
                              IModelValidator validator)
    {
        _serviceTypeRepository = serviceTypeRepository;
        _validator = validator;
    }

    public async Task<ResponseResult<ServiceTypeSummaryDto>> CreateServiceType(CreateServiceTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateServiceTypeDtoValidator, CreateServiceTypeDto>(model, CancellationToken.None);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        ServiceType serviceType = new(model.Name);

        _serviceTypeRepository.AddServiceType(serviceType);

        await _serviceTypeRepository.SaveChangesAsync();

        ServiceTypeSummaryDto serviceTypeSummaryDto = new(serviceType.Id, serviceType.Name);

        return new(serviceTypeSummaryDto);
    }

    public async Task<ResponseResult> DeleteServiceType(Guid serviceTypeId)
    {
        var serviceType = await _serviceTypeRepository.GetBySpec(new ServiceTypeDeleteSpec(serviceTypeId));

        if (serviceType is null) return new(new NotFoundException(nameof(serviceTypeId), "Service Type", serviceTypeId));

        // TODO: validate if any service of this service type is in use before deleting
        serviceType.Delete();

        await _serviceTypeRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<ServiceTypeSummaryDto>> GetServiceTypeId(Guid id, CancellationToken token)
    {
        var serviceTypeDto = await _serviceTypeRepository.GetProjectedEntityBySpec(new ServiceTypeByIdSpec(id), token);

        if (serviceTypeDto is null) return new(new NotFoundException(nameof(id), "Service Type", id));

        return new(serviceTypeDto);
    }

    public async Task<ResponseResult<IReadOnlyList<ServiceTypeDto>>> GetServiceTypes(Paginator paginator,
                                                                                     ServiceTypeFilter filter,
                                                                                     CancellationToken token)
    {
        var (list, totalRecords) = await _serviceTypeRepository.GetProjectedListBySpec(paginator, new ServiceTypeListSpec(filter), token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult> Update(Guid serviceTypeId, UpdateServiceTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<UpdateServiceTypeDtoValidator, UpdateServiceTypeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var serviceType = await _serviceTypeRepository.GetBySpec(new ServiceTypeUpdateSpec(serviceTypeId));

        if (serviceType is null) return new(new NotFoundException(nameof(serviceTypeId), "Service Type", serviceTypeId));

        serviceType.Update(model.Name);

        await _serviceTypeRepository.SaveChangesAsync();



        return new();
    }
}
