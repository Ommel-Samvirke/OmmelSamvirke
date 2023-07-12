using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class SaveTemporaryPageTemplateCommand : IRequest<PageTemplateDto>
{
    public PageTemplateDto OriginalPageTemplate { get; }
    public PageTemplateDto UpdatedPageTemplate { get; }

    public SaveTemporaryPageTemplateCommand(PageTemplateDto originalPageTemplate, PageTemplateDto updatedPageTemplate)
    {
        OriginalPageTemplate = originalPageTemplate;
        UpdatedPageTemplate = updatedPageTemplate;
    }
}

public class SaveTemporaryPageTemplateCommandHandler : IRequestHandler<SaveTemporaryPageTemplateCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public SaveTemporaryPageTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateDto> Handle(SaveTemporaryPageTemplateCommand request, CancellationToken cancellationToken)
    {
        SaveTemporaryPageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate currentPageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.OriginalPageTemplate.Id))!;
        PageTemplateDto currentPageTemplateDto = _mapper.Map<PageTemplateDto>(currentPageTemplate);
        
        if (!currentPageTemplateDto.Equals(request.OriginalPageTemplate))
            throw new ResourceHasChangedException("The Page Template has changed since you last loaded it");

        PageTemplate tempPageTemplate = await _pageTemplateRepository.GetTempByIdAsync(request.UpdatedPageTemplate.Id);
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(tempPageTemplate);
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
