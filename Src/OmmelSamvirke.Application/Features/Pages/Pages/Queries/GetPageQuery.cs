using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetPageQuery : IRequest<PageQueryDto>
{
    public int PageId { get; init; }
}

public class GetPageQueryHandler : IRequestHandler<GetPageQuery, PageQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public GetPageQueryHandler(IMapper mapper, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageQueryDto> Handle(GetPageQuery request, CancellationToken cancellationToken)
    {
        GetPageQueryValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        Page page = (await _pageRepository.GetByIdAsync(request.PageId))!;
        return _mapper.Map<PageQueryDto>(page);
    }
}
