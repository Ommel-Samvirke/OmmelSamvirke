using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;

public class ImageBlockDataDto : IContentBlockDataDto
{
    public int Id { get; set; }
    public int BaseContentBlockId { get; set; }
    public int PageId { get; set;  }
    public string? ImageUrl { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.ImageBlock;
}
