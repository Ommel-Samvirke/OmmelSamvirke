using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="HeadlineBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class HeadlineBlockData : ContentBlockData<HeadlineBlock>
{
    /// <summary>
    /// The headline text for the block. Must be 1-200 characters long.
    /// </summary>
    public string Headline { get; set; } = string.Empty;
}
