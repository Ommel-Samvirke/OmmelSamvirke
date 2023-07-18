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
    public PageTemplateQueryDto OriginalPageTemplate { get; init; } = new();
    public PageTemplateUpdateDto UpdatedPageTemplate { get; init; } = new();
}

public class UpdatePageTemplateCommandHandler : IRequestHandler<UpdatePageTemplateCommand, PageTemplateQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IContentBlockLayoutConfigurationRepository _contentBlockLayoutConfigurationRepository;

    public UpdatePageTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IPageRepository pageRepository,
        IContentBlockRepository contentBlockRepository,
        IContentBlockLayoutConfigurationRepository contentBlockLayoutConfigurationRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
        _contentBlockRepository = contentBlockRepository;
        _contentBlockLayoutConfigurationRepository = contentBlockLayoutConfigurationRepository;
    }
    
    public async Task<PageTemplateQueryDto> Handle(UpdatePageTemplateCommand request, CancellationToken cancellationToken)
    {
        UpdatePageTemplateCommandValidator validator = new(_mapper, _pageTemplateRepository, _contentBlockRepository, _pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate currentPageTemplate = (await _pageTemplateRepository.GetByIdAsyncWithNavigationProps(
            request.OriginalPageTemplate.Id,
            cancellationToken
        ))!;
        PageTemplateQueryDto currentPageTemplateDto = _mapper.Map<PageTemplateQueryDto>(currentPageTemplate);
        
        if (!currentPageTemplateDto.Equals(request.OriginalPageTemplate))
            throw new ResourceHasChangedException("The Page Template has changed since you last loaded it");

        await DeleteRemovedContentBlocks(request, currentPageTemplateDto);
        await DeleteRemovedContentBlockLayoutConfigurations(request, currentPageTemplate);

        currentPageTemplate.ContentBlocks = _mapper.Map<List<ContentBlock>>(request.UpdatedPageTemplate.ContentBlocks);
        currentPageTemplate.Name = request.UpdatedPageTemplate.Name;
        currentPageTemplate.State = request.UpdatedPageTemplate.State;
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(currentPageTemplate, cancellationToken);

        return _mapper.Map<PageTemplateQueryDto>(updatedPageTemplate);
    }

    private async Task DeleteRemovedContentBlockLayoutConfigurations(UpdatePageTemplateCommand request,
        PageTemplate currentPageTemplate)
    {
        foreach (ContentBlock contentBlock in currentPageTemplate.ContentBlocks)
        {
            if (request.UpdatedPageTemplate.ContentBlocks.Any(updatedContentBlock =>
                    updatedContentBlock.Id == contentBlock.Id)) continue;
            if (contentBlock.DesktopConfiguration != null)
                await _contentBlockLayoutConfigurationRepository.DeleteAsync(contentBlock.DesktopConfiguration);
            if (contentBlock.TabletConfiguration != null)
                await _contentBlockLayoutConfigurationRepository.DeleteAsync(contentBlock.TabletConfiguration);
            if (contentBlock.MobileConfiguration != null)
                await _contentBlockLayoutConfigurationRepository.DeleteAsync(contentBlock.MobileConfiguration);
        }
    }

    private async Task DeleteRemovedContentBlocks(UpdatePageTemplateCommand request,
        PageTemplateQueryDto currentPageTemplateDto)
    {
        IEnumerable<ContentBlockQueryDto> contentBlocksToRemove = currentPageTemplateDto.ContentBlocks
            .Where(contentBlock =>
                request.UpdatedPageTemplate.ContentBlocks.All(updatedContentBlock =>
                    updatedContentBlock.Id != contentBlock.Id));
        await _contentBlockRepository.DeleteAsync(_mapper.Map<List<ContentBlock>>(contentBlocksToRemove));
    }
}
