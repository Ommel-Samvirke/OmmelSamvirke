using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

public class ImageBlock : ContentBlock
{
    public string ImageUrl { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.ImageBlock;
}
