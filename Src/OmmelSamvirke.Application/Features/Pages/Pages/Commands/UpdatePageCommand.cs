﻿using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageCommand : IRequest<PageDto>
{
    public PageDto OriginalPage { get; }
    public PageDto UpdatedPage { get; }
    public List<IContentBlockData> UpdatedContentBlockDataElements { get; }

    public UpdatePageCommand(PageDto originalPage, PageDto updatedPage, List<IContentBlockData> updatedContentBlockDataElements)
    {
        OriginalPage = originalPage;
        UpdatedPage = updatedPage;
        UpdatedContentBlockDataElements = updatedContentBlockDataElements;
    }
}

public class SaveTemporaryPageCommandHandler : IRequestHandler<UpdatePageCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public SaveTemporaryPageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IContentBlockDataRepository contentBlockDataRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _contentBlockDataRepository = contentBlockDataRepository;
    }
    
    public async Task<PageDto> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
    {
        UpdatePageCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page currentPage = (await _pageRepository.GetByIdAsync(request.OriginalPage.Id))!;
        PageDto currentPageDto = _mapper.Map<PageDto>(currentPage);

        if (!currentPageDto.Equals(request.OriginalPage))
            throw new ResourceHasChangedException("The Page has changed since you last loaded it");
        
        List<IContentBlockData> contentBlockDataOriginal =
            await _contentBlockDataRepository.GetByPageIdAsync(request.UpdatedPage.Id);
        
        List<IContentBlockData> newContentBlockData = 
            request.UpdatedContentBlockDataElements
                .Where(updatedContentBlockData => contentBlockDataOriginal
                .All(originalData => originalData.BaseContentBlock.Id != updatedContentBlockData.BaseContentBlock.Id)).ToList();
        
        List<IContentBlockData> deletedContentBlocks = 
            contentBlockDataOriginal
                .Where(originalContentBlockData => request.UpdatedContentBlockDataElements
                .All(newData => newData.BaseContentBlock.Id != originalContentBlockData.BaseContentBlock.Id)).ToList();
        
        await _contentBlockDataRepository.CreateAsync(newContentBlockData);
        await _contentBlockDataRepository.DeleteAsync(deletedContentBlocks);
        
        Page updatedPage = await _pageRepository.UpdateAsync(_mapper.Map<Page>(request.UpdatedPage));
        return _mapper.Map<PageDto>(updatedPage);
    }
}
