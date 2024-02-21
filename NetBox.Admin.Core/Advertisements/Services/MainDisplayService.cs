using NetBox.Admin.Core.Advertisements.DTOs;
using NetBox.Admin.Core.Advertisements.Interfaces;
using NetBox.Admin.Core.Advertisements.Specs;
using NetBox.Admin.Core.Advertisements.Validators;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Advertisements.Services;

sealed class MainDisplayService(IAdvertismentRepository _advertismentRepository,
                                         IFileService _fileService,
                                         IModelValidator _validator,
                                         IApplicationContext applicationContext) : IMainDisplayService
{
    private readonly IAdvertismentRepository _advertismentRepository = _advertismentRepository;
    private readonly IFileService _fileService = _fileService;
    private readonly IModelValidator _validator = _validator;
    private readonly IApplicationContext _applicationContext = applicationContext;

    public async Task<ResponseResult<AdvertisementDto>> Create(CreateAdvertismentDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateAdvertismentDtoValidator, CreateAdvertismentDto>(model, CancellationToken.None);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var relativePath = await _fileService.AddDocuments(model.file, AppConstants.Folders.Advertisements);

        var isImage = FileHelper.IsImageFile(model.file.FileName);

        Advertisement entity = new(relativePath, isImage);

        _advertismentRepository.Add(entity);

        await _advertismentRepository.SaveChangesAsync();

        return new(new AdvertisementDto(entity.Id, entity.FilelURL, entity.IsImage));
    }

    public async Task<ResponseResult> Delete(Guid id)
    {
        var advertisment = await _advertismentRepository.GetBySpec(new AdvertisementDeleteSpec(id));

        if (advertisment is null) return new(new NotFoundException(nameof(id), "Advertisement", id));

        advertisment.Delete();

        var response = _fileService.DeleteFile(advertisment.FilelURL);

        if (response.Success is false) return response;

        await _advertismentRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<IReadOnlyList<AdvertisementDto>>> GetList(Paginator paginator, CancellationToken token)
    {
        var (list, totalRecords) = await _advertismentRepository.GetProjectedListBySpec(paginator, new AdvertisementListSpec(), token);

        IList<AdvertisementDto> result = [];
        foreach (var item in list)
        {
            var obj = item with { FileURL = _applicationContext.BaseUrl + item.FileURL };
            result.Add(obj);
        }

        return new(result.AsReadOnly(), totalRecords);
    }
}
