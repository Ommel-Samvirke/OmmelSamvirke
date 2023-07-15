using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs;

public class ContentBlockDto
{
    public int Id { get; set; }
    public bool IsOptional { get; set; }
    public ContentBlockLayoutConfigurationDto DesktopConfiguration { get; set; }
    public ContentBlockLayoutConfigurationDto TabletConfiguration { get; set; }
    public ContentBlockLayoutConfigurationDto MobileConfiguration { get; set; }
    public ContentBlockType ContentBlockType { get; set; }
    public PageTemplateDto PageTemplate { get; set; }

    public ContentBlockDto(
        int id,
        bool isOptional,
        ContentBlockLayoutConfigurationDto desktopConfiguration,
        ContentBlockLayoutConfigurationDto tabletConfiguration,
        ContentBlockLayoutConfigurationDto mobileConfiguration,
        ContentBlockType contentBlockType,
        PageTemplateDto pageTemplate
    )
    {
        Id = id;
        IsOptional = isOptional;
        DesktopConfiguration = desktopConfiguration;
        TabletConfiguration = tabletConfiguration;
        MobileConfiguration = mobileConfiguration;
        ContentBlockType = contentBlockType;
        PageTemplate = pageTemplate;
    }
}
