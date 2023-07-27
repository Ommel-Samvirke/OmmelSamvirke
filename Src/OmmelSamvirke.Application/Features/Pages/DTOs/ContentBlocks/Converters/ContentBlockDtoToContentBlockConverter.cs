using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks.Converters;

[UsedImplicitly]
public class ContentBlockDtoToContentBlockConverter : ITypeConverter<ContentBlockDto, ContentBlock>
{
    public ContentBlock Convert(ContentBlockDto source, ContentBlock destination, ResolutionContext context)
    {
        return source switch
        {
            HeadLineBlockDto headlineBlockDto => context.Mapper.Map<HeadlineBlock>(headlineBlockDto),
            ImageBlockDto imageBlockDto => context.Mapper.Map<ImageBlock>(imageBlockDto),
            PdfBlockDto pdfBlockDto => context.Mapper.Map<PdfBlock>(pdfBlockDto),
            SlideshowBlockDto slideshowBlockDto => context.Mapper.Map<SlideshowBlock>(slideshowBlockDto),
            TextBlockDto textBlockDto => context.Mapper.Map<TextBlock>(textBlockDto),
            VideoBlockDto videoBlockDto => context.Mapper.Map<VideoBlock>(videoBlockDto),
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
    }
}
