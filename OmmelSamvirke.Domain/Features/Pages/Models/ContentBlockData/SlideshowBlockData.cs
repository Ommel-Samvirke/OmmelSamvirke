using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

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
    public List<string> ImageUrls { get; private set; } = null!;

    /// <summary>
    /// Create a new instance of a SlideshowBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="slideshowBlock"><see cref="ContentBlock"/></param>
    /// <param name="imageUrls"><see cref="ImageUrls"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public SlideshowBlockData(
        SlideshowBlock slideshowBlock,
        List<string> imageUrls,
        int pageId
    ) : base(slideshowBlock, pageId)
    {
        Initialize(imageUrls);
    }
    
    /// <summary>
    /// Create an instance of a SlideshowBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="slideshowBlock"><see cref="ContentBlock"/></param>
    /// <param name="imageUrls"><see cref="ImageUrls"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public SlideshowBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        SlideshowBlock slideshowBlock,
        List<string> imageUrls,
        int pageId
    ) : base(id, dateCreated, dateModified, slideshowBlock, pageId)
    {
        Initialize(imageUrls);
    }

    private void Initialize(List<string> imageUrls)
    {
        ImageUrls = imageUrls;
    }
}
