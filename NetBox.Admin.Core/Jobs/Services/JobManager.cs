using NetBox.Admin.Core.Customers.DTOs;
using NetBox.Admin.Core.Customers.Interfaces;
using NetBox.Admin.Core.Jobs.DTOs;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.Core.Jobs.Validators;

namespace NetBox.Admin.Core.Jobs.Services;
sealed class JobManager : IJobManager
{
    private readonly ICusotmerService _cusotmerService;
    private readonly ITokenNumberGenerator _tokenNumberGenerator;
    private readonly IUnitOfWork _uow;
    private readonly IModelValidator _validator;
    private readonly IDesignerJobService _designerJobService;

    public JobManager(ICusotmerService cusotmerService,
                      ITokenNumberGenerator tokenNumberGenerator,
                      IUnitOfWork uow,
                      IModelValidator validator,
                      IDesignerJobService designerJobService)
    {
        _cusotmerService = cusotmerService;
        _tokenNumberGenerator = tokenNumberGenerator;
        _uow = uow;
        _validator = validator;
        _designerJobService = designerJobService;
    }

    public async Task<ResponseResult<JobDTO>> CreateJob(JobCreateDTO model)
    {

        var validationResult = await _validator.ValidateAsync<JobCreateDTOValidator, JobCreateDTO>(model, CancellationToken.None);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        CustomerDTO customer;

        using (var trnx = await _uow.BeginTransaction())
            try
            {
                var customerResponse = await AddOrUpdateCustomer();

                if (customerResponse.Success is false) return new(customerResponse.Errors);

                customer = customerResponse.Data!;

                var tokenNumber = await _tokenNumberGenerator.GetNextTokenNumber();

                CreateJobInternalDTO newJob = new(customer.Id,
                                                  customer.PhoneNumber,
                                                  tokenNumber.Date,
                                                  tokenNumber.TokenNumber,
                                                  model.ClientType,
                                                  model.DesignerId,
                                                  model.DesignSentByModeId,
                                                  model.JobType);

                var newJobResponse = _designerJobService.CreateJob(newJob);

                if (newJobResponse.Success is false) return new(newJobResponse.Errors);

                await _uow.SaveChangesAsync();
                await trnx.CommitAsync();

                return newJobResponse;
            }
            catch (Exception)
            {
                await trnx.RollbackAsync();
                return new(new OperationFailedException("Token Creation", "Failed to create Token"));
            }

        async Task<ResponseResult<CustomerDTO>> AddOrUpdateCustomer()
        {
            if (model.CustomerId is null)
            {
                var customerResponse = await _cusotmerService.Create(new(model.PhoneNumber!, model.CustomerName));

                if (customerResponse.Success is false) return new(customerResponse.Errors);
                return customerResponse;
            }

            else
            {
                var customerResponse = await _cusotmerService.Update(new(model.CustomerId.Value, model.CustomerName));
                if (customerResponse.Success is false) return new(customerResponse.Errors);
                return customerResponse;
            }
        }
    }
}
