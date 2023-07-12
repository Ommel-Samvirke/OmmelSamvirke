using AutoMapper;
using FluentValidation;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetNextPageQuery : IRequest<PageDto>
{
    public int CommunityId { get; }
    public int CurrentPageId { get; }

    public GetNextPageQuery(int communityId, int currentPageId)
    {
        CommunityId = communityId;
        CurrentPageId = currentPageId;
    }
}

public class GetNextPageQueryHandler : IRequestHandler<GetNextPageQuery, PageDto>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;
    private readonly IPageRepository _pageRepository;

    public GetNextPageQueryHandler(IMapper mapper, ICommunityRepository communityRepository, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(GetNextPageQuery request, CancellationToken cancellationToken)
    {
        GetNextPageQueryValidator validator = new(_communityRepository, _pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = await _communityRepository.GetNextPage(request.CommunityId, request.CurrentPageId);
        return _mapper.Map<PageDto>(page);
    }
}
