using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetContentBlockQuery : IRequest<List<ContentBlockDto>>
{
    public int LayoutConfigurationId { get; init; }
}

public class GetContentBlockQueryHandler : IRequestHandler<GetContentBlockQuery, List<ContentBlockDto>>
{
    private readonly IMapper _mapper;
    private readonly IContentBlockRepository<ContentBlock> _contentBlockRepository;
    private readonly ILayoutConfigurationRepository _layoutConfigurationRepository;

    public GetContentBlockQueryHandler(
        IMapper mapper,
        IContentBlockRepository<ContentBlock> contentBlockRepository,
        ILayoutConfigurationRepository layoutConfigurationRepository
    )
    {
        _mapper = mapper;
        _contentBlockRepository = contentBlockRepository;
        _layoutConfigurationRepository = layoutConfigurationRepository;
    }
    
    public async Task<List<ContentBlockDto>> Handle(GetContentBlockQuery request, CancellationToken cancellationToken)
    {
        GetContentBlockDataQueryValidator validator = new(_layoutConfigurationRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        return _mapper.Map<List<ContentBlockDto>>(
            await _contentBlockRepository.GetByLayoutConfiguration(request.LayoutConfigurationId, cancellationToken
        ));
    }
}
