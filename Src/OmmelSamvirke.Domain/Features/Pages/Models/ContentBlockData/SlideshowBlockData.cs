using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="SlideshowBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class SlideshowBlockData : ContentBlockData<SlideshowBlock>
{
    /// <summary>
    /// The list of image URLs for the slideshow block.
    /// </summary>
    public List<Url> ImageUrls { get; set; } = new();
}
