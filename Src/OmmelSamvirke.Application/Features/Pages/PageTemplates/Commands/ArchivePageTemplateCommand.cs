using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class ArchivePageTemplateCommand : IRequest<PageTemplateQueryDto>
{
    public int PageTemplateId { get; init; }
}

public class ArchivePageTemplateCommandHandler : IRequestHandler<ArchivePageTemplateCommand, PageTemplateQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public ArchivePageTemplateCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateQueryDto> Handle(ArchivePageTemplateCommand request, CancellationToken cancellationToken)
    {
        ArchivePageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;
        pageTemplate.State = PageTemplateState.Archived;
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.UpdateAsync(pageTemplate);
        return _mapper.Map<PageTemplateQueryDto>(updatedPageTemplate);
    }
}
