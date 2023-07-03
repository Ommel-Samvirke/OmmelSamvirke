using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class CreatePageTemplateFromPageCommand : IRequest<PageTemplateDto>
{
    public Page Page { get; }

    public CreatePageTemplateFromPageCommand(Page page)
    {
        Page = page;
    }
}

public class CreatePageTemplateFromPageCommandHandler : IRequestHandler<CreatePageTemplateFromPageCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public CreatePageTemplateFromPageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockDataRepository contentBlockDataRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockDataRepository = contentBlockDataRepository;
    }
    
    public async Task<PageTemplateDto> Handle(CreatePageTemplateFromPageCommand request, CancellationToken cancellationToken)
    {
        CreatePageTemplateFromPageCommandValidator validator = new(_pageRepository, _contentBlockDataRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        List<IContentBlockData> contentBlockData = await _contentBlockDataRepository.GetByPageIdAsync(
            (int)request.Page.Id!
        );

        List<ContentBlock> contentBlocks = contentBlockData.Select(
            contentBlockDataItem => contentBlockDataItem.BaseContentBlock
        ).ToList();
        
        PageTemplate customPageTemplate = new(
            $"{request.Page.Name}",
            request.Page.Template.SupportedLayouts,
            contentBlocks,
            PageTemplateState.Custom
        );

        PageTemplate createdTemplate = await _pageTemplateRepository.CreateAsync(customPageTemplate);
        request.Page.Template = createdTemplate;
        
        await _pageRepository.UpdateAsync(request.Page);

        return _mapper.Map<PageTemplateDto>(createdTemplate);
    }
}
