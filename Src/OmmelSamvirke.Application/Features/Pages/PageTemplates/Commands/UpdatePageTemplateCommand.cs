using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class UpdatePageTemplateCommand : IRequest<PageTemplateQueryDto>
{
    public PageTemplateQueryDto OriginalPageTemplate { get; }
    public PageTemplateUpdateDto UpdatedPageTemplate { get; }

    public UpdatePageTemplateCommand(PageTemplateQueryDto originalPageTemplate, PageTemplateUpdateDto updatedPageTemplate)
    {
        OriginalPageTemplate = originalPageTemplate;
        UpdatedPageTemplate = updatedPageTemplate;
    }
}

public class SaveTemporaryPageTemplateCommandHandler : IRequestHandler<UpdatePageTemplateCommand, PageTemplateQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;

    public SaveTemporaryPageTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
    }
    
    public async Task<PageTemplateQueryDto> Handle(UpdatePageTemplateCommand request, CancellationToken cancellationToken)
    {
        UpdatePageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate currentPageTemplate = (await _pageTemplateRepository.GetByIdAsync((int)request.OriginalPageTemplate.Id!))!;
        PageTemplateQueryDto currentPageTemplateDto = _mapper.Map<PageTemplateQueryDto>(currentPageTemplate);
        
        if (!currentPageTemplateDto.Equals(request.OriginalPageTemplate))
            throw new ResourceHasChangedException("The Page Template has changed since you last loaded it");
        
        List<ContentBlockCreateDto> newContentBlocks = 
            request.UpdatedPageTemplate.ContentBlocks
                .Where(updatedContentBlock => currentPageTemplateDto.ContentBlocks
                .All(originalContentBlock => ContentBlocksHaveSameDesktopPosition(originalContentBlock, updatedContentBlock))).ToList();
        
        List<ContentBlockQueryDto> deletedContentBlocks = 
            currentPageTemplateDto.ContentBlocks
                .Where(originalContentBlock => request.UpdatedPageTemplate.ContentBlocks
                .All(updatedContentBlock => ContentBlocksHaveSameDesktopPosition(originalContentBlock, updatedContentBlock))).ToList();

        await _contentBlockRepository.CreateAsync(_mapper.Map<List<ContentBlock>>(newContentBlocks));
        await _contentBlockRepository.DeleteAsync(_mapper.Map<List<ContentBlock>>(deletedContentBlocks));
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(_mapper.Map<PageTemplate>(request.UpdatedPageTemplate));
        return _mapper.Map<PageTemplateQueryDto>(updatedPageTemplate);
    }
    
    private static bool ContentBlocksHaveSameDesktopPosition(ContentBlockQueryDto contentBlock1, ContentBlockCreateDto contentBlock2)
    {
        return contentBlock1.DesktopConfiguration.XPosition == contentBlock2.DesktopConfiguration.XPosition &&
               contentBlock1.DesktopConfiguration.YPosition == contentBlock2.DesktopConfiguration.YPosition;
    }
}
