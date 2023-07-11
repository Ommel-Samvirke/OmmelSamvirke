using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;

public class GetPageTemplateCommand : IRequest<PageTemplateDto>
{
    public int PageTemplateId { get; }

    public GetPageTemplateCommand(int pageTemplateId)
    {
        PageTemplateId = pageTemplateId;
    }
}

public class GetPageTemplateCommandHandler : IRequestHandler<GetPageTemplateCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public GetPageTemplateCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateDto> Handle(GetPageTemplateCommand request, CancellationToken cancellationToken)
    {
        GetPageTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;
        return _mapper.Map<PageTemplateDto>(pageTemplate);
    }
}
