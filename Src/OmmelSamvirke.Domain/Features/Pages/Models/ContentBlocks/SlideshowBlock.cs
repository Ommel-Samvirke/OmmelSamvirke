using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a slideshow block that can be placed on a page.
/// It extends the abstract ContentBlock class.
/// </summary>
public class SlideshowBlock : ContentBlock
{
    public List<Url> ImageUrls { get; set; } = new();
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.SlideshowBlock;
}
