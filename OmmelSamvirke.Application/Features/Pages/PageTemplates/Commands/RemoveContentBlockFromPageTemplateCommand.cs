using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class RemoveContentBlockFromPageTemplateCommand : IRequest<PageTemplateDto>
{
    public PageTemplate PageTemplate { get; }
    public ContentBlock ContentBlock { get; }

    public RemoveContentBlockFromPageTemplateCommand(PageTemplate pageTemplate, ContentBlock contentBlock)
    {
        PageTemplate = pageTemplate;
        ContentBlock = contentBlock;
    }
}

public class RemoveContentBlockFromPageTemplateCommandHandler : IRequestHandler<RemoveContentBlockFromPageTemplateCommand, PageTemplateDto>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IMapper _mapper;

    public RemoveContentBlockFromPageTemplateCommandHandler(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IMapper mapper    
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _mapper = mapper;
    }
    
    public async Task<PageTemplateDto> Handle(RemoveContentBlockFromPageTemplateCommand request, CancellationToken cancellationToken)
    {
        RemoveContentBlockFromPageTemplateCommandValidator validator = new(_pageTemplateRepository, _contentBlockRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync((int)request.PageTemplate.Id!))!;
        pageTemplate.Blocks.Remove(request.ContentBlock);
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(pageTemplate);
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
