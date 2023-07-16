using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPagesByCommunityIdQuery : IRequest<List<PageQueryDto>>
{
    public int CommunityId { get; init; }
}

public class GetPagesByCommunityIdQueryHandler : IRequestHandler<GetPagesByCommunityIdQuery, List<PageQueryDto>>
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
    
    public async Task<List<PageQueryDto>> Handle(GetPagesByCommunityIdQuery request, CancellationToken cancellationToken)
    {
        GetPagesByCommunityIdQueryValidator validator = new(_communityRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        List<Page> pages = await _communityRepository.GetPages(request.CommunityId);
        return _mapper.Map<List<PageQueryDto>>(pages);
    }
}
