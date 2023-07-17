using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;

public class TextBlockDataDto : IContentBlockDataDto
{
    public int Id { get; set; }
    public int BaseContentBlockId { get; set; }
    public int PageId { get; set;  }
    public string? Text { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.TextBlock;
}
