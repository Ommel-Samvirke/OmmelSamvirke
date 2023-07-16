using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class ContentBlockQueryDto : BaseModel
{
    public bool IsOptional { get; set; }
    public ContentBlockLayoutConfigurationQueryDto DesktopConfiguration { get; set; }
    public ContentBlockLayoutConfigurationQueryDto TabletConfiguration { get; set; }
    public ContentBlockLayoutConfigurationQueryDto MobileConfiguration { get; set; }
    public ContentBlockType ContentBlockType { get; set; }

    public ContentBlockQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        bool isOptional,
        ContentBlockLayoutConfigurationQueryDto desktopConfiguration,
        ContentBlockLayoutConfigurationQueryDto tabletConfiguration,
        ContentBlockLayoutConfigurationQueryDto mobileConfiguration,
        ContentBlockType contentBlockType
    ) : base(id, createdDate, modifiedDate)
    {
        IsOptional = isOptional;
        DesktopConfiguration = desktopConfiguration;
        TabletConfiguration = tabletConfiguration;
        MobileConfiguration = mobileConfiguration;
        ContentBlockType = contentBlockType;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ContentBlockQueryDto()
    {
        
    }
}
