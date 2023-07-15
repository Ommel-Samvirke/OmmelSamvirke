using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ValueResolvers;

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
            DateTime datePlaceholder = DateTime.Now;
            PageTemplate pageTemplate = context.Mapper.Map<PageTemplate>(cbDto.PageTemplate);

            ContentBlock contentBlock = cbDto.ContentBlockType switch
            {
                ContentBlockType.HeadlineBlock => 
                    new HeadlineBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                ContentBlockType.ImageBlock => 
                    new ImageBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                ContentBlockType.PdfBlock => 
                    new PdfBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                ContentBlockType.SlideshowBlock => 
                    new SlideshowBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                ContentBlockType.TextBlock => 
                    new TextBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                ContentBlockType.VideoBlock => 
                    new VideoBlock(
                    cbDto.Id,
                    datePlaceholder,
                    datePlaceholder,
                    cbDto.IsOptional,
                    desktopConfig,
                    tabletConfig,
                    mobileConfig,
                    pageTemplate
                ),
                _ => throw new NotSupportedException($"ContentBlockType {cbDto.ContentBlockType} not supported")
            };

            return contentBlock;
        }).ToList();
    }
}
