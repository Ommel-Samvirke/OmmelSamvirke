using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class ContentBlockCreateDto
{
    public bool IsOptional { get; set; }
    public ContentBlockLayoutConfigurationCreateDto DesktopConfiguration { get; set; }
    public ContentBlockLayoutConfigurationCreateDto TabletConfiguration { get; set; }
    public ContentBlockLayoutConfigurationCreateDto MobileConfiguration { get; set; }
    public ContentBlockType ContentBlockType { get; set; }
    public int PageTemplateId { get; set; }

    public ContentBlockCreateDto(
        bool isOptional,
        ContentBlockLayoutConfigurationCreateDto desktopConfiguration,
        ContentBlockLayoutConfigurationCreateDto tabletConfiguration,
        ContentBlockLayoutConfigurationCreateDto mobileConfiguration,
        ContentBlockType contentBlockType,
        int pageTemplateId
    )
    {
        IsOptional = isOptional;
        DesktopConfiguration = desktopConfiguration;
        TabletConfiguration = tabletConfiguration;
        MobileConfiguration = mobileConfiguration;
        ContentBlockType = contentBlockType;
        PageTemplateId = pageTemplateId;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ContentBlockCreateDto()
    {
        
    }
}
