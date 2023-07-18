﻿using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.Converters;

[UsedImplicitly]
public class ContentBlockCreateDtoToContentBlockConverter : ITypeConverter<ContentBlockCreateDto, ContentBlock>
{
    public ContentBlock Convert(ContentBlockCreateDto source, ContentBlock destination, ResolutionContext context)
    {
        ContentBlock contentBlock = source.ContentBlockType switch
        {
            ContentBlockType.HeadlineBlock => context.Mapper.Map<HeadlineBlock>(source),
            ContentBlockType.ImageBlock => context.Mapper.Map<ImageBlock>(source),
            ContentBlockType.PdfBlock => context.Mapper.Map<PdfBlock>(source),
            ContentBlockType.SlideshowBlock => context.Mapper.Map<SlideshowBlock>(source),
            ContentBlockType.TextBlock => context.Mapper.Map<TextBlock>(source),
            ContentBlockType.VideoBlock => context.Mapper.Map<VideoBlock>(source),
            _ => throw new ArgumentException("Invalid ContentBlock type")
        };
        
        contentBlock.DesktopConfiguration = context.Mapper.Map<ContentBlockLayoutConfiguration>(source.DesktopConfiguration);
        contentBlock.TabletConfiguration = context.Mapper.Map<ContentBlockLayoutConfiguration>(source.TabletConfiguration);
        contentBlock.MobileConfiguration = context.Mapper.Map<ContentBlockLayoutConfiguration>(source.MobileConfiguration);
        
        return contentBlock;
    }
}
