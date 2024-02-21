namespace NetBox.Admin.SharedKernal.Helpers;

public static class FileHelper
{
    public static string GetFileExtension(string fileName)
    {
        var extension = fileName.Split(".").Last();

        return $".{extension}";
    }

    public static bool IsImageFile(string fileName)
    {
        var ext = GetFileExtension(fileName);

        bool isImage = AppConstants.FileExtension.ValidImageFileExtensions.Any(z => z == ext);

        return isImage;
    }
}
