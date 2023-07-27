using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks.Converters;

[UsedImplicitly]
public class ContentBlockToContentBlockDtoConverter : ITypeConverter<ContentBlock, ContentBlockDto>
{
    public ContentBlockDto Convert(ContentBlock source, ContentBlockDto destination, ResolutionContext context)
    {
        return source switch
        {
            HeadlineBlock headlineBlock => context.Mapper.Map<HeadLineBlockDto>(headlineBlock),
            ImageBlock imageBlock => context.Mapper.Map<ImageBlockDto>(imageBlock),
            PdfBlock pdfBlock => context.Mapper.Map<PdfBlockDto>(pdfBlock),
            SlideshowBlock slideshowBlock => context.Mapper.Map<SlideshowBlockDto>(slideshowBlock),
            TextBlock textBlock => context.Mapper.Map<TextBlockDto>(textBlock),
            VideoBlock videoBlock => context.Mapper.Map<VideoBlockDto>(videoBlock),
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
    }
}
