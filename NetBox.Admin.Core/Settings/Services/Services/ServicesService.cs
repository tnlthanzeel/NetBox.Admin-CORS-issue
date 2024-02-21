using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Interfaces;
using NetBox.Admin.Core.Settings.Services.Specs;
using NetBox.Admin.Core.Settings.Services.Validators;

namespace NetBox.Admin.Core.Settings.Services.Services;

internal sealed class ServicesService(IServiceTypeRepository _serviceTypeRepository,
                                      IServicesRepository _servicesRepository,
                                      IModelValidator _validator) : IServicesService
{
    private readonly IServiceTypeRepository _serviceTypeRepository = _serviceTypeRepository;
    private readonly IServicesRepository _servicesRepository = _servicesRepository;
    private readonly IModelValidator _validator = _validator;

    public async Task<ResponseResult<ServiceDto>> CreateService(Guid serviceTypeId, CreateServiceDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateServiceDtoValidator, CreateServiceDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var serviceType = await _serviceTypeRepository.GetBySpec(new ServiceTypeByIdToAddNewServiceSpec(serviceTypeId));

        if (serviceType is null) return new(new NotFoundException(nameof(serviceTypeId), "Service Type", serviceTypeId));

        serviceType.AddService(model.Name, model.Rate);

        await _serviceTypeRepository.SaveChangesAsync();

        var service = serviceType.Services.First();

        ServiceDto serviceDto = new(service.ServiceTypeId, service.Id, service.Name,service.Rate);

        return new(serviceDto);
    }

    public async Task<ResponseResult> DeleteService(Guid serviceTypeId, Guid serviceId)
    {
        var serviceType = await _serviceTypeRepository.GetBySpec(new ServiceDeleteSpec(serviceTypeId, serviceId));

        if (serviceType is null || serviceType.Services.Count is 0) return new(new NotFoundException(nameof(serviceId), "Service", serviceId));

        // TODO: validate if any service is in use before deleting
        serviceType.DeleteService();

        await _serviceTypeRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<ServiceDto>> GetServiceById(Guid serviceTypeId, Guid id, CancellationToken token)
    {
        var serviceDto = await _serviceTypeRepository.GetProjectedEntityBySpec(new ServiceByIdSpec(serviceTypeId, id), token);

        if (serviceDto is null) return new(new NotFoundException(nameof(id), "Service", id));

        return new(serviceDto);
    }

    public async Task<ResponseResult<IReadOnlyList<ServiceDto>>> GetServices(Guid serviceTypeId, Paginator paginator, CancellationToken token)
    {
        var (list, totalRecords) = await _servicesRepository.GetProjectedListBySpec(paginator, new ServiceListSpec(serviceTypeId), token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult> UpdateService(Guid serviceTypeId, Guid serviceId, UpdateServiceDto model)
    {
        var validationResult = await _validator.ValidateAsync<UpdateServiceDtoValidator, UpdateServiceDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var serviceType = await _serviceTypeRepository.GetBySpec(new ServiceUpdateSpec(serviceTypeId, serviceId));

        if (serviceType is null || serviceType.Services.Count is 0) return new(new NotFoundException(nameof(serviceId), "Service", serviceId));

        serviceType.UpdateService(model.Name, model.Rate);

        await _serviceTypeRepository.SaveChangesAsync();

        return new();
    }
}
