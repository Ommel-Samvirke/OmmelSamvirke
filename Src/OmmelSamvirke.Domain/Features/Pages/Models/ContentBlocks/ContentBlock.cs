using JsonSubTypes;
using Newtonsoft.Json;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a content block that can be placed on a page. 
/// It serves as a base class for more specific types of content blocks.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), "ContentBlockType")]
[JsonSubtypes.KnownSubType(typeof(HeadlineBlock), (int)ContentBlockType.HeadlineBlock)]
[JsonSubtypes.KnownSubType(typeof(ImageBlock), (int)ContentBlockType.ImageBlock)]
[JsonSubtypes.KnownSubType(typeof(PdfBlock), (int)ContentBlockType.PdfBlock)]
[JsonSubtypes.KnownSubType(typeof(SlideshowBlock), (int)ContentBlockType.SlideshowBlock)]
[JsonSubtypes.KnownSubType(typeof(TextBlock), (int)ContentBlockType.TextBlock)]
[JsonSubtypes.KnownSubType(typeof(VideoBlock), (int)ContentBlockType.VideoBlock)]
public abstract class ContentBlock : BaseModel
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
