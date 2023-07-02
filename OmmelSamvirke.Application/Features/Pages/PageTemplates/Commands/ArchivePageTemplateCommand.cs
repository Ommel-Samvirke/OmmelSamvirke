using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class ArchivePageTemplateCommand : IRequest<PageTemplateDto>
{
    public int PageTemplateId { get; }
    public PageTemplateState CurrentTemplate { get; }
    
    public ArchivePageTemplateCommand(int pageTemplateId, PageTemplateState currentTemplate)
    {
        PageTemplateId = pageTemplateId;
        CurrentTemplate = currentTemplate;
    }
}

public class ArchivePageTemplateCommandHandler : IRequestHandler<ArchivePageTemplateCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public ArchivePageTemplateCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateDto> Handle(ArchivePageTemplateCommand request, CancellationToken cancellationToken)
    {
        ArchivePageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;
        pageTemplate.State = PageTemplateState.Archived;
        await _pageTemplateRepository.UpdateAsync(pageTemplate);

        return _mapper.Map<PageTemplateDto>(pageTemplate);
    }
}
