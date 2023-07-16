using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

public class ContentBlockLayoutConfiguration : BaseModel
{
    /// <summary>
    /// The X position of the block on the page.
    /// </summary>
    public int XPosition { get; set; }

    /// <summary>
    /// The Y position of the block on the page.
    /// </summary>
    public int YPosition { get; set; }

    /// <summary>
    /// The width of the block.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the block.
    /// </summary>
    public int Height { get; set; }
}
