using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.Converters;

[UsedImplicitly]
public class ContentBlockDtoToContentBlockConverter : ITypeConverter<ContentBlockDto, ContentBlock>
{
    public ContentBlock Convert(ContentBlockDto source, ContentBlock destination, ResolutionContext context)
    {
        return source.ContentBlockType switch
        {
            ContentBlockType.HeadlineBlock => context.Mapper.Map<HeadlineBlock>(source),
            ContentBlockType.ImageBlock => context.Mapper.Map<ImageBlock>(source),
            ContentBlockType.PdfBlock => context.Mapper.Map<PdfBlock>(source),
            ContentBlockType.SlideshowBlock => context.Mapper.Map<SlideshowBlock>(source),
            ContentBlockType.TextBlock => context.Mapper.Map<TextBlock>(source),
            ContentBlockType.VideoBlock => context.Mapper.Map<VideoBlock>(source),
            _ => throw new ArgumentException("Invalid ContentBlock type")
        };
    }
}
