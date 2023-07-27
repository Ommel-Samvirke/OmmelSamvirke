using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs;

public class PageDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public LayoutConfigurationDto DesktopConfiguration { get; set; } = new();
    public int DesktopConfigurationId { get; set; }
    public LayoutConfigurationDto TabletConfiguration { get; set; } = new();
    public int TabletConfigurationId { get; set; }
    public LayoutConfigurationDto MobileConfiguration { get; set; } = new();
    public int MobileConfigurationId { get; set; }
    public int CommunityId { get; set; }
    public PageState State { get; set; }
}
