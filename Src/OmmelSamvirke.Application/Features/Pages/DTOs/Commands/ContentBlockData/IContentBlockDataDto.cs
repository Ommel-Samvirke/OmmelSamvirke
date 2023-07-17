using JsonSubTypes;
using Newtonsoft.Json;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;

[JsonConverter(typeof(JsonSubtypes), "ContentBlockType")]
[JsonSubtypes.KnownSubType(typeof(HeadlineBlockDataDto), "HeadlineBlockData")]
[JsonSubtypes.KnownSubType(typeof(ImageBlockDataDto), "ImageBlockData")]
[JsonSubtypes.KnownSubType(typeof(PdfBlockDataDto), "PdfBlockData")]
[JsonSubtypes.KnownSubType(typeof(SlideshowBlockDataDto), "SlideshowBlockData")]
[JsonSubtypes.KnownSubType(typeof(TextBlockDataDto), "TextBlockData")]
[JsonSubtypes.KnownSubType(typeof(VideoBlockDataDto), "VideoBlockData")]
public interface IContentBlockDataDto
{
    public int Id { get; set; }
    public int BaseContentBlockId { get; set; }
    public int PageId { get; set;  }
    public ContentBlockType ContentBlockType { get; set; }
}
