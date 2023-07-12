using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPageQuery : IRequest<PageDto>
{
    public int PageId { get; }

    public GetPageQuery(int pageId)
    {
        PageId = pageId;
    }
}

public class GetPageQueryHandler : IRequestHandler<GetPageQuery, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public GetPageQueryHandler(IMapper mapper, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(GetPageQuery request, CancellationToken cancellationToken)
    {
        GetPageQueryValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = (await _pageRepository.GetByIdAsync(request.PageId))!;
        return _mapper.Map<PageDto>(page);
    }
}
