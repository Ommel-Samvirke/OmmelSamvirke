using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class CreatePageFromTemplateCommand : IRequest<PageDto>
{
    public PageTemplateDto PageTemplateDto { get; }
    public string PageName { get; }

    public CreatePageFromTemplateCommand(PageTemplateDto pageTemplateDto, String pageName)
    {
        PageTemplateDto = pageTemplateDto;
        PageName = pageName;
    }
}

public class CreatePageFromTemplateCommandHandler : IRequestHandler<CreatePageFromTemplateCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;

    public CreatePageFromTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IPageRepository pageRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(CreatePageFromTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageFromTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateDto.Id))!;
        Page page = new(request.PageName, pageTemplate);
        
        Page createdPage = await _pageRepository.CreateAsync(page);
        return _mapper.Map<PageDto>(createdPage);
    }
}
