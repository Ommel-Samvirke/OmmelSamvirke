using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ValueResolvers;

[UsedImplicitly]
public class DtoToContentBlockResolver : IValueResolver<PageTemplateQueryDto, PageTemplate, List<ContentBlock>>
{
    public List<ContentBlock> Resolve(PageTemplateQueryDto source, PageTemplate destination, List<ContentBlock> destMember, ResolutionContext context)
    {
        return source.ContentBlocks.Select(cbDto => 
        {
            ContentBlockLayoutConfiguration desktopConfig =
                context.Mapper.Map<ContentBlockLayoutConfiguration>(cbDto.DesktopConfiguration);
            ContentBlockLayoutConfiguration tabletConfig =
                context.Mapper.Map<ContentBlockLayoutConfiguration>(cbDto.TabletConfiguration);
            ContentBlockLayoutConfiguration mobileConfig =
                context.Mapper.Map<ContentBlockLayoutConfiguration>(cbDto.MobileConfiguration);

            ContentBlock contentBlock = cbDto.ContentBlockType switch
            {
                ContentBlockType.HeadlineBlock => new HeadlineBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                ContentBlockType.ImageBlock => new ImageBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                ContentBlockType.PdfBlock => new PdfBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                ContentBlockType.SlideshowBlock => new SlideshowBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                ContentBlockType.TextBlock => new TextBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                ContentBlockType.VideoBlock => new VideoBlock(
                    (int)cbDto.Id!,
                    cbDto.DateCreated,
                    cbDto.DateModified,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig
                ),
                _ => throw new NotSupportedException($"ContentBlockType {cbDto.ContentBlockType} not supported")
            };

            return contentBlock;
        }).ToList();
    }
}
