using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with an <see cref="ImageBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class ImageBlockData : ContentBlockData<ImageBlock>
{
    /// <summary>
    /// The URL of the image for the block. Must be 1-2000 characters long.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}
