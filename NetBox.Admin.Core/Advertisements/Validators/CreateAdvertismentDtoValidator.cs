using NetBox.Admin.Core.Advertisements.DTOs;

namespace NetBox.Admin.Core.Advertisements.Validators;

public sealed class CreateAdvertismentDtoValidator : AbstractValidator<CreateAdvertismentDto>
{
    public CreateAdvertismentDtoValidator()
    {
        RuleFor(f => f.file)
            .NotEmpty()
            .WithMessage("A file must be attached")
            .ChildRules(file =>
            {
                file.RuleFor(f => f.FileName).NotEmpty().WithMessage("File name is required");
                file.RuleFor(c => c.FileName).Must((file, fileName) =>
                {
                    var fileExtension = FileHelper.GetFileExtension(fileName);
                    var isValidExtension = AppConstants.FileExtension.ValidAdvertisementFileExtensions.Any(ext => ext == fileExtension.ToLower());
                    return isValidExtension;
                }).WithMessage("Unsupported file format");
            });
    }
}