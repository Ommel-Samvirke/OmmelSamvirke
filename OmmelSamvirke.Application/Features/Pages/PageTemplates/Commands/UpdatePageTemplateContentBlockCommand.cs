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
    public PageTemplateDto PageTemplate { get; }
    public ContentBlockDto ContentBlock { get; }

    public UpdatePageTemplateContentBlockCommand(PageTemplateDto pageTemplate, ContentBlockDto contentBlock)
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
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplate.Id))!;
        ContentBlock contentBlock = (await _contentBlockRepository.GetByIdAsync(request.ContentBlock.Id))!;
        pageTemplate.ContentBlocks.Remove(pageTemplate.ContentBlocks.First(x => x.Id == contentBlock.Id));
        pageTemplate.ContentBlocks.Add(contentBlock);
        
        await _pageTemplateRepository.UpdateAsync(pageTemplate);
        await _contentBlockRepository.UpdateAsync(contentBlock);

        return _mapper.Map<PageTemplateDto>(pageTemplate);
    }
}
