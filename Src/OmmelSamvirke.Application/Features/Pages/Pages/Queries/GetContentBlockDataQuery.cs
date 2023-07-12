using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Queries;

public class GetContentBlockDataQuery : IRequest<List<ContentBlockDataDto>>
{
    public int PageId { get; }

    public GetContentBlockDataQuery(int pageId)
    {
        PageId = pageId;
    }
}

public class GetContentBlockDataQueryHandler : IRequestHandler<GetContentBlockDataQuery, List<ContentBlockDataDto>>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public GetContentBlockDataQueryHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IContentBlockDataRepository contentBlockDataRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _contentBlockDataRepository = contentBlockDataRepository;
    }
    
    public async Task<List<ContentBlockDataDto>> Handle(GetContentBlockDataQuery request, CancellationToken cancellationToken)
    {
        GetContentBlockDataQueryValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        List<IContentBlockData> contentBlockData = await _contentBlockDataRepository.GetByPageIdAsync(request.PageId);
        return MapContentBlockData(contentBlockData);
    }
    
    private List<ContentBlockDataDto> MapContentBlockData(List<IContentBlockData> contentBlockData)
    {
        List<ContentBlockDataDto> contentBlockDataDtos = new();
        
        foreach (IContentBlockData dataElement in contentBlockData)
        {
            switch (dataElement)
            {
                case HeadlineBlockData headlineBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<HeadlineBlockDataDto>(headlineBlockData));
                    break;
                case ImageBlockData imageBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<ImageBlockDataDto>(imageBlockData));
                    break;
                case PdfBlockData pdfBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<PdfBlockDataDto>(pdfBlockData));
                    break;
                case SlideshowBlockData slideshowBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<SlideshowBlockDataDto>(slideshowBlockData));
                    break;
                case TextBlockData textBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<TextBlockDataDto>(textBlockData));
                    break;
                case VideoBlockData videoBlockData:
                    contentBlockDataDtos.Add(_mapper.Map<VideoBlockDataDto>(videoBlockData));
                    break;
            }
        }

        return contentBlockDataDtos;
    }
}
