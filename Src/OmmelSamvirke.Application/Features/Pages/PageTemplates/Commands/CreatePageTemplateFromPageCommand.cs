using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class CreatePageTemplateFromPageCommand : IRequest<PageTemplateQueryDto>
{
    public PageUpdateDto PageUpdateDto { get; set; } = new();
}

public class CreatePageTemplateFromPageCommandHandler : IRequestHandler<CreatePageTemplateFromPageCommand, PageTemplateQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;

    public CreatePageTemplateFromPageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;
    }
    
    public async Task<PageTemplateQueryDto> Handle(CreatePageTemplateFromPageCommand request, CancellationToken cancellationToken)
    {
        CreatePageTemplateFromPageCommandValidator validator = new(_pageRepository, _pageTemplateRepository, _contentBlockDataRepositoriesAggregate);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = (await _pageRepository.GetByIdAsync(request.PageUpdateDto.Id, cancellationToken))!;
        List<IContentBlockData> contentBlockData = await _contentBlockDataRepositoriesAggregate.GetByPageIdAsync(
            request.PageUpdateDto.Id,
            cancellationToken
        );

        List<ContentBlock> contentBlocks = contentBlockData.Select(
            contentBlockDataItem => contentBlockDataItem.BaseContentBlock
        ).Where(c => c is not null).ToList()!;

        PageTemplate customPageTemplate = new()
        {
            Name = $"{request.PageUpdateDto.Name}-template",
            ContentBlocks = contentBlocks,
            State = PageTemplateState.Custom
        };
        
        PageTemplate createdTemplate = await _pageTemplateRepository.CreateAsync(customPageTemplate, cancellationToken);
        page.TemplateId = createdTemplate.Id;
        
        await _pageRepository.UpdateAsync(page, cancellationToken);

        return _mapper.Map<PageTemplateQueryDto>(createdTemplate);
    }
}
