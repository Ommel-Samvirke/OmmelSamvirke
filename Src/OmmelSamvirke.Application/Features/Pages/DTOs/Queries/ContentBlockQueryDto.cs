using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class ContentBlockQueryDto : BaseModel
{
    public bool IsOptional { get; set; }
    public ContentBlockLayoutConfigurationQueryDto DesktopConfiguration { get; set; } = new();
    public ContentBlockLayoutConfigurationQueryDto TabletConfiguration { get; set; } = new();
    public ContentBlockLayoutConfigurationQueryDto MobileConfiguration { get; set; } = new();
    public ContentBlockType ContentBlockType { get; set; }
}
