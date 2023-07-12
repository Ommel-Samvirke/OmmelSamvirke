using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class CreatePageTemplateCommand : IRequest<PageTemplateDto>
{
    public string Name { get; }
    public List<ContentBlockDto> ContentBlocks { get;}
    public PageTemplateState PageTemplateState { get; }
    
    public CreatePageTemplateCommand(
        string name,
        List<ContentBlockDto> contentBlocks,
        PageTemplateState pageTemplateState
    )
    {
        Name = name;
        ContentBlocks = contentBlocks;
        PageTemplateState = pageTemplateState;
    }
}

public class CreatePageTemplateCommandHandler : IRequestHandler<CreatePageTemplateCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public CreatePageTemplateCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateDto> Handle(CreatePageTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageTemplateCommandValidator validator = new();
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate requestPage = _mapper.Map<PageTemplate>(request);
        
        PageTemplate createdTemplate = await _pageTemplateRepository.CreateAsync(requestPage);
        return _mapper.Map<PageTemplateDto>(createdTemplate);
    }
}
