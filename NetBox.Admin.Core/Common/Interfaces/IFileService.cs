
namespace NetBox.Admin.Core.Common.Interfaces;

public interface IFileService
{
    Task<string> AddDocuments(IFormFile file, string folderName);
    ResponseResult DeleteFile(string filelURL);
}
