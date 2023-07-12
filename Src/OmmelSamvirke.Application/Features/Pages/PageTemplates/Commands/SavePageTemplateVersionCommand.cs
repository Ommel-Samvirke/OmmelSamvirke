using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class SavePageTemplateVersionCommand : IRequest<PageTemplateDto>
{
    public PageTemplateDto PageTemplateVersion { get; }

    public SavePageTemplateVersionCommand(PageTemplateDto pageTemplateVersion)
    {
        PageTemplateVersion = pageTemplateVersion;
    }
}

public class SavePageTemplateVersionCommandHandler : IRequestHandler<SavePageTemplateVersionCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public SavePageTemplateVersionCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async Task<PageTemplateDto> Handle(SavePageTemplateVersionCommand request, CancellationToken cancellationToken)
    {
        SavePageTemplateVersionCommandValidator validator = new();
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        bool versionExists = await _pageTemplateRepository.GetVersionAsync(request.PageTemplateVersion.Id) is not null;
        if (versionExists)
            throw new BadRequestException("Version cannot be saved, because it already exists");
        
        PageTemplate pageTemplate = _mapper.Map<PageTemplate>(request.PageTemplateVersion);
        PageTemplate savedPageTemplateVersion = await _pageTemplateRepository.SaveVersionAsync(pageTemplate);
        
        return _mapper.Map<PageTemplateDto>(savedPageTemplateVersion);
    }
}
