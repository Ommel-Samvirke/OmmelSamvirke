using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class UpdatePageTemplateContentBlockCommand : IRequest<PageTemplateDto>
{
    public PageTemplate PageTemplate { get; }
    public ContentBlock ContentBlock { get; }

    public UpdatePageTemplateContentBlockCommand(PageTemplate pageTemplate, ContentBlock contentBlock)
    {
        PageTemplate = pageTemplate;
        ContentBlock = contentBlock;
    }
}

public class UpdatePageTemplateContentBlockCommandHandler : IRequestHandler<UpdatePageTemplateContentBlockCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;

    public UpdatePageTemplateContentBlockCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
    }
    
    public async Task<PageTemplateDto> Handle(UpdatePageTemplateContentBlockCommand request, CancellationToken cancellationToken)
    {
        UpdatePageTemplateContentBlockCommandValidator validator = new(_pageTemplateRepository, _contentBlockRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync((int)request.PageTemplate.Id!))!;
        pageTemplate.Blocks.Remove(pageTemplate.Blocks.First(x => x.Id == request.ContentBlock.Id));
        pageTemplate.Blocks.Add(request.ContentBlock);
        
        await _pageTemplateRepository.UpdateAsync(pageTemplate);
        await _contentBlockRepository.UpdateAsync(request.ContentBlock);

        return _mapper.Map<PageTemplateDto>(pageTemplate);
    }
}
