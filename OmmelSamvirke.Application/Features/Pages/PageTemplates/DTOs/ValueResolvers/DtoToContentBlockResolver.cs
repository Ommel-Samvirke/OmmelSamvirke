using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.ValueResolvers;

[UsedImplicitly]
public class DtoToContentBlockResolver : IValueResolver<PageTemplateDto, PageTemplate, List<ContentBlock>>
{
    public List<ContentBlock> Resolve(PageTemplateDto source, PageTemplate destination, List<ContentBlock> destMember, ResolutionContext context)
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
                ContentBlockType.HeadlineBlock => 
                    new HeadlineBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                ContentBlockType.ImageBlock => 
                    new ImageBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                ContentBlockType.PdfBlock => 
                    new PdfBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                ContentBlockType.SlideshowBlock => 
                    new SlideshowBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                ContentBlockType.TextBlock => 
                    new TextBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                ContentBlockType.VideoBlock => 
                    new VideoBlock(cbDto.IsOptional, desktopConfig, tabletConfig, mobileConfig),
                _ => throw new NotSupportedException($"ContentBlockType {cbDto.ContentBlockType} not supported")
            };
            
            contentBlock.Id = cbDto.Id;

            return contentBlock;
        }).ToList();
    }
}
