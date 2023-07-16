using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="TextBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class TextBlockData : ContentBlockData<TextBlock>
{
    /// <summary>
    /// The text content for the text block.
    /// </summary>
    public string Text { get; set; } = string.Empty;
}
