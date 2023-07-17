using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class UpdatePageCommandExample : IExamplesProvider<UpdatePageCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private const int PageId = 9;

    public UpdatePageCommandExample(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public UpdatePageCommand GetExamples()
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        
        IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        IPageRepository pageRepository = scope.ServiceProvider.GetRequiredService<IPageRepository>();
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate = 
            scope.ServiceProvider.GetRequiredService<IContentBlockDataRepositoriesAggregate>();
        
        Page? page = Task.Run(() => pageRepository.GetByIdAsync(PageId)).Result;
        
        PageQueryDto originalPage = GetOriginalPage(mapper, page);
        PageUpdateDto updatedPage = CreateUpdatedPage(page);
        List<IContentBlockDataDto> updatedContentBlockDataElements = CreateUpdatedContentBlockDataElements(
            mapper,
            contentBlockDataRepositoriesAggregate,
            page
        );

        return new UpdatePageCommand
        {
            OriginalPage = originalPage,
            UpdatedPage = updatedPage,
            UpdatedContentBlockDataElements = updatedContentBlockDataElements,
        };
    }

    private static PageQueryDto GetOriginalPage(IMapper mapper, Page? page)
    {
        return page is null ? null! : mapper.Map<PageQueryDto>(page);
    }

    private static PageUpdateDto CreateUpdatedPage(Page? page)
    {
        if (page is null)
            return null!;

        Random random = new();

        return new PageUpdateDto
        {
            Id = PageId,
            Name = $"Updated Page {random.NextInt64(100)}",
            PageTemplateId = page.TemplateId,
        };
    }
    
    private static List<IContentBlockDataDto> CreateUpdatedContentBlockDataElements(
        IMapper mapper,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepository,
        Page? page
    )
    {
        if (page is null)
            return new List<IContentBlockDataDto>();

        Random random = new();

        List<IContentBlockData> contentBlockDataElements = 
            Task.Run(() => contentBlockDataRepository.GetByPageIdAsync(page.Id)).Result;
        
        List<IContentBlockDataDto> updatedContentBlockDataElements = new();

        foreach (IContentBlockData contentBlockDataElement in contentBlockDataElements)
        {
            switch (contentBlockDataElement)
            {
                case HeadlineBlockData hbd:
                    HeadlineBlockDataDto headlineBlockDataDto = mapper.Map<HeadlineBlockDataDto>(hbd);
                    headlineBlockDataDto.Headline = $"Updated Headline {random.NextInt64(100)}";
                    updatedContentBlockDataElements.Add(headlineBlockDataDto);
                    break;
                case ImageBlockData ibd:
                    ImageBlockDataDto imageBlockDataDto = mapper.Map<ImageBlockDataDto>(ibd);
                    imageBlockDataDto.ImageUrl = $"https://example.com/image-{random.NextInt64(100)}.png";
                    updatedContentBlockDataElements.Add(imageBlockDataDto);
                    break;
                case PdfBlockData pbd:
                    PdfBlockDataDto pdfBlockDataDto = mapper.Map<PdfBlockDataDto>(pbd);
                    pdfBlockDataDto.PdfUrl = $"https://example.com/pdf-{random.NextInt64(100)}.pdf";
                    updatedContentBlockDataElements.Add(pdfBlockDataDto);
                    break;
                case SlideshowBlockData sbd:
                    SlideshowBlockDataDto slideshowBlockDataDto = mapper.Map<SlideshowBlockDataDto>(sbd);
                    slideshowBlockDataDto.ImageUrls = new List<Url>
                    {
                        new($"https://example.com/slideshow-{random.NextInt64(100)}.png"),
                        new($"https://example.com/slideshow-{random.NextInt64(100)}.png"),
                        new($"https://example.com/slideshow-{random.NextInt64(100)}.png"),
                    };
                    updatedContentBlockDataElements.Add(slideshowBlockDataDto);
                    break;
                case TextBlockData tbd:
                    TextBlockDataDto textBlockDataDto = mapper.Map<TextBlockDataDto>(tbd);
                    textBlockDataDto.Text = $"Updated Text {random.NextInt64(100)}";
                    updatedContentBlockDataElements.Add(textBlockDataDto);
                    break;
                case VideoBlockData vbd:
                    VideoBlockDataDto videoBlockDataDto = mapper.Map<VideoBlockDataDto>(vbd);
                    videoBlockDataDto.VideoUrl = $"https://example.com/video-{random.NextInt64(100)}.mp4";
                    updatedContentBlockDataElements.Add(videoBlockDataDto);
                    break;
            }
        }

        return updatedContentBlockDataElements;
    }
}
