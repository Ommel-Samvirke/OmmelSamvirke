using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class AddContentBlockToPageTemplateCommand : IRequest<PageTemplateDto>
{
    public ContentBlock ContentBlock { get; }
    public PageTemplate PageTemplate { get; }
    
    public AddContentBlockToPageTemplateCommand(PageTemplate pageTemplate, ContentBlock contentBlock)
    {
        PageTemplate = pageTemplate;
        ContentBlock = contentBlock;
    }
}

public class AddContentBlockToPageTemplateCommandHandler : IRequestHandler<AddContentBlockToPageTemplateCommand, PageTemplateDto>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IMapper _mapper;

    public AddContentBlockToPageTemplateCommandHandler(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IMapper mapper
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _mapper = mapper;
    }
    public async Task<PageTemplateDto> Handle(AddContentBlockToPageTemplateCommand request, CancellationToken cancellationToken)
    {
        AddContentBlockToPageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        ContentBlock contentBlock = await _contentBlockRepository.CreateAsync(request.ContentBlock);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync((int)request.PageTemplate.Id!))!;
        pageTemplate.Blocks.Add(contentBlock);
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(pageTemplate);
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
