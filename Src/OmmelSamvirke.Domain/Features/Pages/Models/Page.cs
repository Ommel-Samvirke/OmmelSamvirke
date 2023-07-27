using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

/// <summary>
/// This class represents a page composed of <see cref="ContentBlock"/> elements.
/// Each page has 3 different layout configurations for Desktop, Tablet and Mobile devices.
/// Each page must have a unique name.
/// </summary>
public class Page : BaseModel
{
    /// <summary>
    /// Describes the name of the page.
    /// Must be 1-100 characters long.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The layout configuration for Desktop devices.
    /// </summary>
    public LayoutConfiguration DesktopConfiguration { get; set; } = new();
    public int DesktopConfigurationId { get; set; }

    /// <summary>
    /// The layout configuration for Tablet devices.
    /// </summary>
    public LayoutConfiguration TabletConfiguration { get; set; }  = new();
    public int TabletConfigurationId { get; set; }

    /// <summary>
    /// The layout configuration for Mobile devices.
    /// </summary>
    public LayoutConfiguration MobileConfiguration { get; set; }  = new();
    public int MobileConfigurationId { get; set; }

    /// <summary>
    /// The id of the community the page belongs to.
    /// </summary>
    public int CommunityId { get; set; }

    /// <summary>
    /// The visibility state of the page. Can be either visible or hidden.
    /// </summary>
    public PageState State { get; set; }
}
