using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class MakePageTemplatePublicCommand : IRequest<PageTemplateDto>
{
    public int PageTemplateId { get; }
    public PageTemplateState CurrentTemplateState { get; }

    public MakePageTemplatePublicCommand(int pageTemplateId, PageTemplateState currentTemplateState)
    {
        PageTemplateId = pageTemplateId;
        CurrentTemplateState = currentTemplateState;
    }
}

public class MakePageTemplatePublicCommandHandler : IRequestHandler<MakePageTemplatePublicCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public MakePageTemplatePublicCommandHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<PageTemplateDto> Handle(MakePageTemplatePublicCommand request, CancellationToken cancellationToken)
    {
        MakePageTemplatePublicCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;
        pageTemplate.State = PageTemplateState.Public;
        await _pageTemplateRepository.UpdateAsync(pageTemplate);

        return _mapper.Map<PageTemplateDto>(pageTemplate);
    }
}
