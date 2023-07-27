using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a text block that can be placed on a page.
/// It extends the abstract ContentBlock class.
/// </summary>
public class TextBlock : ContentBlock
{
    public string Text { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.TextBlock;
    // TODO Add properties for horizontal and vertical alignment, font color, etc.
}
