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
                ContentBlockType.HeadlineBlock => new HeadlineBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                ContentBlockType.ImageBlock => new ImageBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                ContentBlockType.PdfBlock => new PdfBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                ContentBlockType.SlideshowBlock => new SlideshowBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                ContentBlockType.TextBlock => new TextBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                ContentBlockType.VideoBlock => new VideoBlock
                {
                    Id = cbDto.Id,
                    DateCreated = cbDto.DateCreated,
                    DateModified = cbDto.DateModified,
                    IsOptional = cbDto.IsOptional,
                    DesktopConfiguration = desktopConfig,
                    TabletConfiguration = tabletConfig,
                    MobileConfiguration = mobileConfig
                },
                _ => throw new NotSupportedException($"ContentBlockType {cbDto.ContentBlockType} not supported")
            };

            return contentBlock;
        }).ToList();
    }
}
