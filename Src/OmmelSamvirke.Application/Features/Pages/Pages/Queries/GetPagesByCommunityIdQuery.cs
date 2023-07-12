using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPagesByCommunityIdQuery : IRequest<List<PageDto>>
{
    public int CommunityId { get; }

    public GetPagesByCommunityIdQuery(int communityId)
    {
        CommunityId = communityId;
    }
}

public class GetPagesByCommunityIdQueryHandler : IRequestHandler<GetPagesByCommunityIdQuery, List<PageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;

    public GetPagesByCommunityIdQueryHandler(
        IMapper mapper,
        ICommunityRepository communityRepository
    )
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
    }
    
    public async Task<List<PageDto>> Handle(GetPagesByCommunityIdQuery request, CancellationToken cancellationToken)
    {
        GetPagesByCommunityIdQueryValidator validator = new(_communityRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        List<Page> pages = await _communityRepository.GetPages(request.CommunityId);
        return _mapper.Map<List<PageDto>>(pages);
    }
}
