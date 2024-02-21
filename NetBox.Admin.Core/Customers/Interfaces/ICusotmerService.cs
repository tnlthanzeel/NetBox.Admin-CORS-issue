using NetBox.Admin.Core.Customers.DTOs;

namespace NetBox.Admin.Core.Customers.Interfaces;
public interface ICusotmerService
{
    Task<ResponseResult<CustomerDTO>> Create(CustomerCreateDTO model);
    Task<ResponseResult<CustomerDTO>> Update(CustomerNameUpdateDTO model);


}
