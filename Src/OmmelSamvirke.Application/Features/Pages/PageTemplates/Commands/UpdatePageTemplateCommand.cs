using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class UpdatePageTemplateCommand : IRequest<PageTemplateDto>
{
    public PageTemplateDto OriginalPageTemplate { get; }
    public PageTemplateDto UpdatedPageTemplate { get; }

    public UpdatePageTemplateCommand(PageTemplateDto originalPageTemplate, PageTemplateDto updatedPageTemplate)
    {
        OriginalPageTemplate = originalPageTemplate;
        UpdatedPageTemplate = updatedPageTemplate;
    }
}

public class SaveTemporaryPageTemplateCommandHandler : IRequestHandler<UpdatePageTemplateCommand, PageTemplateDto>
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
    
    public async Task<PageTemplateDto> Handle(UpdatePageTemplateCommand request, CancellationToken cancellationToken)
    {
        UpdatePageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate currentPageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.OriginalPageTemplate.Id))!;
        PageTemplateDto currentPageTemplateDto = _mapper.Map<PageTemplateDto>(currentPageTemplate);
        
        if (!currentPageTemplateDto.Equals(request.OriginalPageTemplate))
            throw new ResourceHasChangedException("The Page Template has changed since you last loaded it");
        
        List<ContentBlockDto> newContentBlocks = 
            request.UpdatedPageTemplate.ContentBlocks
                .Where(updatedContentBlock => currentPageTemplateDto.ContentBlocks
                .All(originalContentBlock => originalContentBlock.Id != updatedContentBlock.Id)).ToList();
        
        List<ContentBlockDto> deletedContentBlocks = 
            currentPageTemplateDto.ContentBlocks
                .Where(originalContentBlock => request.UpdatedPageTemplate.ContentBlocks
                .All(updatedContentBlock => updatedContentBlock.Id != originalContentBlock.Id)).ToList();

        await _contentBlockRepository.CreateAsync(_mapper.Map<List<ContentBlock>>(newContentBlocks));
        await _contentBlockRepository.DeleteAsync(_mapper.Map<List<ContentBlock>>(deletedContentBlocks));
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(_mapper.Map<PageTemplate>(request.UpdatedPageTemplate));
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
