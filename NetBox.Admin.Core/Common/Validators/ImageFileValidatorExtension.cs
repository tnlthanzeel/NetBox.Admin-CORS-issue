namespace NetBox.Admin.Core.Common.Validators;

internal static class ImageFileValidatorExtension
{
    internal static IRuleBuilderOptions<T, IFormFile?> ValidateImageFile<T>(this IRuleBuilder<T, IFormFile?> rule)
    {
        return rule.ChildRules(file =>
                    {
                        file.RuleFor(f => f!.FileName).NotEmpty().WithMessage("Image name is required");
                        file.RuleFor(c => c!.FileName).Must((file, fileName) =>
                        {
                            var fileExtension = FileHelper.GetFileExtension(fileName);
                            var isValidExtension = AppConstants.FileExtension.ValidImageFileExtensions.Any(ext => ext == fileExtension.ToLower());
                            return isValidExtension;
                        }).WithMessage("Unsupported image type");
                    });
    }
}
