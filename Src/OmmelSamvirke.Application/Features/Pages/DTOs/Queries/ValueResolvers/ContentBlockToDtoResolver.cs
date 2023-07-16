using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ValueResolvers;

[UsedImplicitly]
public class ContentBlockToDtoResolver : IValueResolver<PageTemplate, PageTemplateQueryDto, List<ContentBlockQueryDto>>
{
    public List<ContentBlockQueryDto> Resolve(PageTemplate source, PageTemplateQueryDto destination, List<ContentBlockQueryDto> destMember, ResolutionContext context)
    {
        return source.ContentBlocks.Select(cb => 
        {
            ContentBlockLayoutConfigurationQueryDto desktopConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationQueryDto>(cb.DesktopConfiguration);
            ContentBlockLayoutConfigurationQueryDto tabletConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationQueryDto>(cb.TabletConfiguration);
            ContentBlockLayoutConfigurationQueryDto mobileConfig =
                context.Mapper.Map<ContentBlockLayoutConfigurationQueryDto>(cb.MobileConfiguration);

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
            
            return new ContentBlockQueryDto(
                (int)cb.Id!,
                cb.DateCreated,
                cb.DateModified,
                cb.IsOptional,
                desktopConfig,
                tabletConfig,
                mobileConfig,
                contentBlockType
            );
        }).ToList();
    }
}
