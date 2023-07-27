using JsonSubTypes;
using Newtonsoft.Json;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;

[JsonConverter(typeof(JsonSubtypes), "ContentBlockType")]
[JsonSubtypes.KnownSubType(typeof(HeadLineBlockDto), (int)ContentBlockType.HeadlineBlock)]
[JsonSubtypes.KnownSubType(typeof(ImageBlockDto), (int)ContentBlockType.ImageBlock)]
[JsonSubtypes.KnownSubType(typeof(PdfBlockDto), (int)ContentBlockType.PdfBlock)]
[JsonSubtypes.KnownSubType(typeof(SlideshowBlockDto), (int)ContentBlockType.SlideshowBlock)]
[JsonSubtypes.KnownSubType(typeof(TextBlockDto), (int)ContentBlockType.TextBlock)]
[JsonSubtypes.KnownSubType(typeof(VideoBlockDto), (int)ContentBlockType.VideoBlock)]
public abstract class ContentBlockDto : BaseModel
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ContentBlockType ContentBlockType { get; set; }
}
