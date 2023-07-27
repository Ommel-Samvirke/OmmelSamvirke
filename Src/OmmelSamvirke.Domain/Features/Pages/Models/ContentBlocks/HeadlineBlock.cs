using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a headline block that can be placed on a page. 
/// It extends the abstract ContentBlock class.
/// </summary>
public class HeadlineBlock : ContentBlock
{
    public string Headline { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.HeadlineBlock;
    // TODO properties for vertical and horizontal alignment, font color, etc.
}
