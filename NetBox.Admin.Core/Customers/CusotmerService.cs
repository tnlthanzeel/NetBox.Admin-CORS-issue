using NetBox.Admin.Core.Customers.DTOs;
using NetBox.Admin.Core.Customers.Interfaces;
using NetBox.Admin.Core.Customers.Specs;
using NetBox.Admin.Core.Customers.Validators;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;
using NetBox.Admin.Core.Settings.ClientTypes.Spec;

namespace NetBox.Admin.Core.Customers;

sealed class CusotmerService : ICusotmerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IModelValidator _validator;

    public CusotmerService(ICustomerRepository customerRepository, IModelValidator validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<ResponseResult<CustomerDTO>> Create(CustomerCreateDTO model)
    {
        var validationResult = await _validator.ValidateAsync<CustomerCreateDTOValidator, CustomerCreateDTO>(model, CancellationToken.None);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        Customer customer = new(model.PhoneNumber, model.Name);

        _customerRepository.Add(customer);

        await _customerRepository.SaveChangesAsync();

        return new(new CustomerDTO(customer.Id, customer.PhoneNumber, customer.Name));
    }

    public async Task<ResponseResult<CustomerDTO>> Update(CustomerNameUpdateDTO model)
    {
        var validationResult = await _validator.ValidateAsync<CustomerNameUpdateDTOValidator, CustomerNameUpdateDTO>(model, CancellationToken.None);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var customer = await _customerRepository.GetBySpec(new CustomerNameUpdateSpec(model.Id));

        if (customer is null) return new(new NotFoundException("CustomerId", "Customer", model.Id));

        customer.UpdateName(model.Name);

        await _customerRepository.SaveChangesAsync();

        return new(new CustomerDTO(customer.Id, customer.PhoneNumber, customer.Name));
    }
}
