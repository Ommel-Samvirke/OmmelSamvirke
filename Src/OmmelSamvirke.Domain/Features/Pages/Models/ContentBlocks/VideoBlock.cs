using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a video block that can be placed on a page.
/// It extends the abstract ContentBlock class.
/// </summary>
public class VideoBlock : ContentBlock
{
    public string VideoUrl { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.VideoBlock;
}
