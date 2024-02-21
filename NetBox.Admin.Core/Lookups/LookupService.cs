using Microsoft.AspNetCore.Identity;
using NetBox.Admin.Core.Customers.Interfaces;
using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Lookups.Filters;
using NetBox.Admin.Core.Lookups.Specs;
using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Lookups;

sealed class LookupService : ILookupService
{
    private readonly IClientTypeRepository _clientTypeRepository;
    private readonly IJobTypeRepository _jobTypeRepository;
    private readonly IDesignSentByModeRepository _designSentByModeRepository;
    private readonly IApplicationContext _applicationContext;
    private readonly ICustomerRepository _customerRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public LookupService(IClientTypeRepository clientTypeRepository,
                         IJobTypeRepository jobTypeRepository,
                         IDesignSentByModeRepository designSentByModeRepository,
                         IApplicationContext applicationContext,
                         ICustomerRepository customerRepository,
                         UserManager<ApplicationUser> userManager)
    {
        _clientTypeRepository = clientTypeRepository;
        _jobTypeRepository = jobTypeRepository;
        _designSentByModeRepository = designSentByModeRepository;
        _applicationContext = applicationContext;
        _customerRepository = customerRepository;
        _userManager = userManager;
    }

    public async Task<ResponseResult<IReadOnlyList<ClientTypeLookupDTO>>> GetClientTypesForLookup(CancellationToken cancellationToken)
    {
        var data = await _clientTypeRepository.GetProjectedListBySpec(new ClientTypeLookupSpec(), cancellationToken);
        return new(data, data.Count);
    }

    public async Task<ResponseResult<IReadOnlyList<DesignSentByModeLookupDTO>>> GetDesignSentByModes(CancellationToken cancellationToken)
    {
        var data = await _designSentByModeRepository.GetProjectedListBySpec(new DesignSentByModeLookupSpec(), cancellationToken);

        IList<DesignSentByModeLookupDTO> items = [];

        foreach (var item in data)
        {
            var obj = item with { ImageURL = $"{_applicationContext.BaseUrl}{item.ImageURL}" };
            items.Add(obj);
        }

        return new(items.AsReadOnly(), items.Count);
    }

    public async Task<ResponseResult<IReadOnlyList<JobTypeLookupDTO>>> GetJobTypesForLookup(CancellationToken cancellationToken)
    {
        var data = await _jobTypeRepository.GetProjectedListBySpec(new JobTypeLookupSpec(), cancellationToken);
        return new(data, data.Count);
    }

    public async Task<ResponseResult<IReadOnlyList<CustomerLookupDTO>>> GetCustomersForLookup(CustomerLookupFilter filter,
                                                                                             CancellationToken cancellationToken)
    {
        Paginator paginator = new() { PageNumber = 1, PageSize = 10 };
        var (list, totalRecords) = await _customerRepository.GetProjectedListBySpec(paginator, new CustomerLookupSpec(filter), cancellationToken);
        return new(list, totalRecords);
    }

    public async Task<ResponseResult<IReadOnlyList<UserLookupDTO>>> GetUsersForLookup(CancellationToken token)
    {
        var users = await _userManager.Users
                                      .Select(s => new UserLookupDTO(s.Id, s.UserProfile.DisplayName))
                                      .ToListAsync(token);

        return new(users.AsReadOnly(), users.Count);
    }
}
