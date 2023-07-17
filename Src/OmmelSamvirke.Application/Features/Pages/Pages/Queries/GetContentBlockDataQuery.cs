using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetContentBlockDataQuery : IRequest<List<ContentBlockDataQueryDto>>
{
    public int PageId { get; init; }
}

public class GetContentBlockDataQueryHandler : IRequestHandler<GetContentBlockDataQuery, List<ContentBlockDataQueryDto>>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;

    public GetContentBlockDataQueryHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;
    }
    
    public async Task<List<ContentBlockDataQueryDto>> Handle(GetContentBlockDataQuery request, CancellationToken cancellationToken)
    {
        GetContentBlockDataQueryValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        List<IContentBlockData> contentBlockData = await _contentBlockDataRepositoriesAggregate.GetByPageIdAsync(request.PageId, cancellationToken);
        return MapContentBlockData(contentBlockData);
    }
    
    private List<ContentBlockDataQueryDto> MapContentBlockData(List<IContentBlockData> contentBlockData)
    {
        List<ContentBlockDataQueryDto> contentBlockDataDtos = new();
        
        foreach (IContentBlockData dataElement in contentBlockData)
        {
            switch (dataElement)
            {
                case HeadlineBlockData headlineBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<HeadlineBlockDataQueryDto>(headlineBlockData));
                    break;
                case ImageBlockData imageBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<ImageBlockDataQueryDto>(imageBlockData));
                    break;
                case PdfBlockData pdfBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<PdfBlockDataQueryDto>(pdfBlockData));
                    break;
                case SlideshowBlockData slideshowBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<SlideshowBlockDataQueryDto>(slideshowBlockData));
                    break;
                case TextBlockData textBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<TextBlockDataQueryDto>(textBlockData));
                    break;
                case VideoBlockData videoBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<VideoBlockDataQueryDto>(videoBlockData));
                    break;
            }
        }

        return contentBlockDataDtos;
    }
}
