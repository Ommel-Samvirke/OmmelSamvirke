using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;

public class SlideshowBlockDataDto : IContentBlockDataDto
{
    public int Id { get; set; }
    public int BaseContentBlockId { get; set; }
    public int PageId { get; set;  }
    public List<Url>? ImageUrls { get; set; } = new();
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.SlideshowBlock;
}
