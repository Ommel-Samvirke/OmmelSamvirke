using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.ValueResolvers;

[UsedImplicitly]
public class ContentBlockToDtoResolver : IValueResolver<PageTemplate, PageTemplateDto, List<ContentBlockDto>>
{
    public List<ContentBlockDto> Resolve(PageTemplate source, PageTemplateDto destination, List<ContentBlockDto> destMember, ResolutionContext context)
    {
        return source.ContentBlocks.Select(cb => 
        {
            ContentBlockLayoutConfigurationDto desktopConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationDto>(cb.DesktopConfiguration);
            ContentBlockLayoutConfigurationDto tabletConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationDto>(cb.TabletConfiguration);
            ContentBlockLayoutConfigurationDto mobileConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationDto>(cb.MobileConfiguration);

            ContentBlockType contentBlockType = cb switch
            {
                HeadlineBlock => ContentBlockType.HeadlineBlock,
                ImageBlock => ContentBlockType.ImageBlock,
                PdfBlock => ContentBlockType.PdfBlock,
                SlideshowBlock => ContentBlockType.SlideshowBlock,
                TextBlock => ContentBlockType.TextBlock,
                VideoBlock => ContentBlockType.VideoBlock,
                _ => throw new ArgumentOutOfRangeException(nameof(cb), cb, "Could not convert ContentBlock to DTO")
            };
            
            return new ContentBlockDto((int)cb.Id!, cb.IsOptional, desktopConfig, tabletConfig, mobileConfig, contentBlockType);
        }).ToList();
    }
}
