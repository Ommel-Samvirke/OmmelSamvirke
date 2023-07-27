using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class CreatePageCommand : IRequest<PageDto>
{
    public PageDto Page { get; init; } = null!;
}

public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly ICommunityRepository _communityRepository;

    public CreatePageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        ICommunityRepository communityRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _communityRepository = communityRepository;
    }
    
    public async Task<PageDto> Handle(CreatePageCommand request, CancellationToken cancellationToken)
    {
        CreatePageCommandValidator validator = new(_communityRepository, _pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = _mapper.Map<Page>(request.Page);
        Page createdPage = await _pageRepository.CreateAsync(page,cancellationToken);

        return _mapper.Map<PageDto>(createdPage);
    }
}
