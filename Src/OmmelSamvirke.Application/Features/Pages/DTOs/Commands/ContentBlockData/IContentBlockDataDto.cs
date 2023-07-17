using JsonSubTypes;
using Newtonsoft.Json;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;

[JsonConverter(typeof(JsonSubtypes), "ContentBlockType")]
[JsonSubtypes.KnownSubType(typeof(HeadlineBlockDataDto), (int)ContentBlockType.HeadlineBlock)]
[JsonSubtypes.KnownSubType(typeof(ImageBlockDataDto), (int)ContentBlockType.ImageBlock)]
[JsonSubtypes.KnownSubType(typeof(PdfBlockDataDto), (int)ContentBlockType.PdfBlock)]
[JsonSubtypes.KnownSubType(typeof(SlideshowBlockDataDto), (int)ContentBlockType.SlideshowBlock)]
[JsonSubtypes.KnownSubType(typeof(TextBlockDataDto), (int)ContentBlockType.TextBlock)]
[JsonSubtypes.KnownSubType(typeof(VideoBlockDataDto), (int)ContentBlockType.VideoBlock)]
public interface IContentBlockDataDto
{
    public int Id { get; set; }
    public int BaseContentBlockId { get; set; }
    public int PageId { get; set;  }
    public ContentBlockType ContentBlockType { get; set; }
}
