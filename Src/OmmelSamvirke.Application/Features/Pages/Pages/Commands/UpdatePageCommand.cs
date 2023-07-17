using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageCommand : IRequest<PageQueryDto>
{
    public PageQueryDto OriginalPage { get; set; } = new();
    public PageUpdateDto UpdatedPage { get; set; } = new();
    public List<IContentBlockData> UpdatedContentBlockDataElements { get; set; } = new();
}

public class SaveTemporaryPageCommandHandler : IRequestHandler<UpdatePageCommand, PageQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;

    public SaveTemporaryPageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;
    }
    
    public async Task<PageQueryDto> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
    {
        UpdatePageCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page currentPage = (await _pageRepository.GetByIdAsync(request.OriginalPage.Id, cancellationToken))!;
        PageQueryDto currentPageDto = _mapper.Map<PageQueryDto>(currentPage);

        if (!currentPageDto.Equals(request.OriginalPage))
            throw new ResourceHasChangedException("The Page has changed since you last loaded it");
        
        List<IContentBlockData> contentBlockDataOriginal =
            await _contentBlockDataRepositoriesAggregate.GetByPageIdAsync(request.UpdatedPage.Id, cancellationToken);
        
        List<IContentBlockData> newContentBlockData = 
            request.UpdatedContentBlockDataElements
                .Where(updatedContentBlockData => contentBlockDataOriginal
                .All(originalData => 
                    updatedContentBlockData.BaseContentBlock != null &&
                    originalData.BaseContentBlock != null &&
                    originalData.BaseContentBlock.Id != updatedContentBlockData.BaseContentBlock.Id)).ToList();
        
        List<IContentBlockData> deletedContentBlocks = 
            contentBlockDataOriginal
                .Where(originalContentBlockData => request.UpdatedContentBlockDataElements
                .All(newData => 
                    originalContentBlockData.BaseContentBlock != null &&
                    newData.BaseContentBlock != null &&
                    newData.BaseContentBlock.Id != originalContentBlockData.BaseContentBlock.Id)).ToList();
        
        await _contentBlockDataRepositoriesAggregate.CreateAsync(newContentBlockData, cancellationToken);
        await _contentBlockDataRepositoriesAggregate.DeleteAsync(deletedContentBlocks, cancellationToken);
        
        Page updatedPage = await _pageRepository.UpdateAsync(_mapper.Map<Page>(request.UpdatedPage), cancellationToken);
        return _mapper.Map<PageQueryDto>(updatedPage);
    }
}
