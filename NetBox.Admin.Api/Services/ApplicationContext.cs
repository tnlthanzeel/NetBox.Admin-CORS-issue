using NetBox.Admin.SharedKernal.Extensions;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Api.Services;

sealed class ApplicationContext : IApplicationContext
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationContext(IWebHostEnvironment webHostEnvironment,
                              IHttpContextAccessor httpContextAccessor)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public string BaseUrl
    {
        get
        {

            var baseUrl = _webHostEnvironment.IsDevelopment() ? _httpContextAccessor.HttpContext?.Request.BaseUrl() :
                                                                _httpContextAccessor.HttpContext?.Request.BaseUrl();

            return baseUrl!;
        }
    }

    public string WebRootPath
    {
        get
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            return webRootPath!;
        }
    }
}
