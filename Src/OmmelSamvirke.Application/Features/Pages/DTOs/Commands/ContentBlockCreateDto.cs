using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class ContentBlockCreateDto : BaseModel
{
    public bool IsOptional { get; set; }
    public ContentBlockLayoutConfigurationCreateDto DesktopConfiguration { get; set; } = new();
    public ContentBlockLayoutConfigurationCreateDto TabletConfiguration { get; set; } = new();
    public ContentBlockLayoutConfigurationCreateDto MobileConfiguration { get; set; } = new();
    public ContentBlockType ContentBlockType { get; set; }
}
