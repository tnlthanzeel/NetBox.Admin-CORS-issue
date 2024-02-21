using NetBox.Admin.SharedKernal.Exceptions;
using NetBox.Admin.SharedKernal.Interfaces;
using NetBox.Admin.SharedKernal.Responses;

namespace NetBox.Admin.Infrastructure.FileServices;

internal sealed class FileService : IFileService
{
    const string UploadFolder = "Uploads/";

    private readonly IApplicationContext _applicationContext;

    public FileService(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<string> AddDocuments(IFormFile file, string filePathToSave)
    {
        string path = Path.Combine(_applicationContext.WebRootPath, UploadFolder + filePathToSave);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileUniqueName = $"{Guid.NewGuid()}{file.FileName}";

        using var stream = File.Create($"{path}/{fileUniqueName}");
        await file.CopyToAsync(stream);

        var relativePath = $"{UploadFolder}{filePathToSave}/{fileUniqueName}";

        return relativePath;
    }

    public ResponseResult DeleteFile(string relativePath)
    {
        try
        {
            var path = $"{_applicationContext.WebRootPath}/{relativePath}";
            File.Delete(path);
            return new();
        }
        catch (Exception)
        {
            return new(new OperationFailedException("File", "Could not delete file"));
        }
    }
}