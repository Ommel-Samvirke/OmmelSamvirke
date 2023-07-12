using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPagesByCommunityIdCommand : IRequest<List<PageDto>>
{
    public int CommunityId { get; }

    public GetPagesByCommunityIdCommand(int communityId)
    {
        CommunityId = communityId;
    }
}

public class GetPagesByCommunityIdCommandHandler : IRequestHandler<GetPagesByCommunityIdCommand, List<PageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;
    private readonly IPageRepository _pageRepository;

    public GetPagesByCommunityIdCommandHandler(
        IMapper mapper,
        ICommunityRepository communityRepository,
        IPageRepository pageRepository
    )
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<List<PageDto>> Handle(GetPagesByCommunityIdCommand request, CancellationToken cancellationToken)
    {
        GetPagesByCommunityIdCommandValidator validator = new(_communityRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        List<Page> pages = await _communityRepository.GetPages(request.CommunityId);
        return _mapper.Map<List<PageDto>>(pages);
    }
}
