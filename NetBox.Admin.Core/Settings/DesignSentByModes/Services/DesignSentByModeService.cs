using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;
using NetBox.Admin.Core.Settings.DesignSentByModes.Specs;
using NetBox.Admin.Core.Settings.DesignSentByModes.Validators;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Services;

internal sealed class DesignSentByModeService(IModelValidator _validator,
                                              IDesignSentByModeRepository _designSentByModeRepository,
                                              IFileService _fileService,
                                              IApplicationContext _applicationContext) : IDesignSentByModeService
{
    private readonly IModelValidator _validator = _validator;
    private readonly IDesignSentByModeRepository _designSentByModeRepository = _designSentByModeRepository;
    private readonly IFileService _fileService = _fileService;
    private readonly IApplicationContext _applicationContext = _applicationContext;

    public async Task<ResponseResult<DesignSentByModeDto>> Create(CreateDesignSentByModeDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateDesignSentByModeDtoValidator, CreateDesignSentByModeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var url = await _fileService.AddDocuments(model.Image, AppConstants.Folders.DesignByModeIcons);

        DesignSentByMode entity = new(model.Mode, url);

        _designSentByModeRepository.Add(entity);

        await _designSentByModeRepository.SaveChangesAsync();

        return new(new DesignSentByModeDto(entity.Id, entity.Mode, entity.ImageURL));
    }

    public async Task<ResponseResult> Delete(Guid id)
    {
        var entity = await _designSentByModeRepository.GetBySpec(new DesignSentByModeDeleteSpec(id));

        if (entity is null) return new(new NotFoundException(nameof(id), "Design Sent By Mode", id));

        // TODO: validate if any design sent by mode is in use before deleting
        entity.Delete();

        await _designSentByModeRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<DesignSentByModeDto>> GetById(Guid id, CancellationToken token)
    {
        var designSentByModeDto = await _designSentByModeRepository.GetProjectedEntityBySpec(new DesignSentByModeByIdSpec(id), token);

        if (designSentByModeDto is null) return new(new NotFoundException(nameof(id), "Design Sent By Mode", id));

        var obj = designSentByModeDto with { ImageURL = _applicationContext.BaseUrl + designSentByModeDto.ImageURL };

        return new(obj);
    }

    public async Task<ResponseResult<IReadOnlyList<DesignSentByModeDto>>> GetList(Paginator paginator, CancellationToken token)
    {

        var (list, totalRecords) = await _designSentByModeRepository.GetProjectedListBySpec(paginator, new DesignSentByModeByListSpec(), token);

        IList<DesignSentByModeDto> designModes = [];

        foreach (var item in list)
        {
            var obj = item with { ImageURL = _applicationContext.BaseUrl + item.ImageURL };
            designModes.Add(obj);
        }

        return new(designModes.AsReadOnly(), totalRecords);
    }

    public async Task<ResponseResult> Update(Guid id, UpdateDesignSentByModeDto model)
    {
        string? url = null;
        var validationResult = await _validator.ValidateAsync<UpdateDesignSentByModeDtoValidator, UpdateDesignSentByModeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var entity = await _designSentByModeRepository.GetBySpec(new DesignSentByModeUpdateSpec(id));

        if (entity is null) return new(new NotFoundException(nameof(id), "Design Sent By Mode", id));

        if (model.Image is not null)
        {
            url = await _fileService.AddDocuments(model.Image, AppConstants.Folders.DesignByModeIcons);
        }

        entity.Update(model.Mode, url);

        await _designSentByModeRepository.SaveChangesAsync();

        return new();
    }
}
