﻿using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPreviousPageQuery : IRequest<PageDto>
{
    public int CommunityId { get; init; }
    public int CurrentPageId { get; init; }
}

public class GetPreviousPageQueryHandler : IRequestHandler<GetPreviousPageQuery, PageDto>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;
    private readonly IPageRepository _pageRepository;

    public GetPreviousPageQueryHandler(IMapper mapper, ICommunityRepository communityRepository, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(GetPreviousPageQuery request, CancellationToken cancellationToken)
    {
        GetPreviousPageQueryValidator validator = new(_communityRepository, _pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = await _communityRepository.GetPreviousPage(request.CommunityId, request.CurrentPageId, cancellationToken);
        return _mapper.Map<PageDto>(page);
    }
}
