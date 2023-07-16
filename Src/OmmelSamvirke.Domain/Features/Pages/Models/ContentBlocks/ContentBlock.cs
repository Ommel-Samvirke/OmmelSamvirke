using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a content block that can be placed on a page. 
/// It serves as a base class for more specific types of content blocks.
/// </summary>
public abstract class ContentBlock : BaseModel
{
    /// <summary>
    /// Whether the block is optional or not.
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// The layout configuration for Desktop devices.
    /// </summary>
    public ContentBlockLayoutConfiguration? DesktopConfiguration { get; set; }

    /// <summary>
    /// The layout configuration for Tablet devices.
    /// </summary>
    public ContentBlockLayoutConfiguration? TabletConfiguration { get; set; }

    /// <summary>
    /// The layout configuration for Mobile devices.
    /// </summary>
    public ContentBlockLayoutConfiguration? MobileConfiguration { get; set; }
}
