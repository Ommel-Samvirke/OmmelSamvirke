using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class CreatePageTemplateCommand : IRequest<PageTemplateQueryDto>
{
    public PageTemplateCreateDto PageTemplateCreateDto { get; init; } = null!;
}

public class CreatePageTemplateCommandHandler : IRequestHandler<CreatePageTemplateCommand, PageTemplateQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public CreatePageTemplateCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateQueryDto> Handle(CreatePageTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageTemplateCommandValidator validator = new();
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate requestPage = _mapper.Map<PageTemplate>(request.PageTemplateCreateDto);
        
        PageTemplate createdTemplate = await _pageTemplateRepository.CreateAsync(requestPage);
        return _mapper.Map<PageTemplateQueryDto>(createdTemplate);
    }
}
