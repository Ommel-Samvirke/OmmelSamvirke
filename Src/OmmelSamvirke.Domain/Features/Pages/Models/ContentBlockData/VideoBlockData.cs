using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

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
    public Url VideoUrl { get; set; } = null!;

    /// <summary>
    /// Create a new instance of a VideoBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="videoBlock"><see cref="ContentBlock"/></param>
    /// <param name="videoUrl"><see cref="VideoUrl"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public VideoBlockData(VideoBlock videoBlock, Url videoUrl, Page page) : base(videoBlock, page)
    {
        Initialize(videoUrl);
    }
    
    /// <summary>
    /// Create an instance of a VideoBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="videoBlock"><see cref="ContentBlock"/></param>
    /// <param name="videoUrl"><see cref="VideoUrl"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public VideoBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        VideoBlock videoBlock,
        Url videoUrl, 
        Page page
    ) : base(id, dateCreated, dateModified, videoBlock, page)
    {
        Initialize(videoUrl);
    }

    private void Initialize(Url videoUrl)
    {
        StringLengthValidator.Validate(videoUrl.Address, 1, 2000);
        VideoUrl = videoUrl;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private VideoBlockData()
    {
        
    }
}
