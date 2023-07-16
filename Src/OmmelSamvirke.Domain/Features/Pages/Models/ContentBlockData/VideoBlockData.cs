using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="VideoBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class VideoBlockData : ContentBlockData<VideoBlock>
{
    /// <summary>
    /// The video URL for the video block.
    /// </summary>
    public string VideoUrl { get; set; } = string.Empty;
}
