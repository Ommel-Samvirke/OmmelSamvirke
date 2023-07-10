using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
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
    public string ImageUrl { get; private set; } = null!;

    /// <summary>
    /// Create a new instance of an ImageBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="imageBlock"><see cref="ContentBlock"/></param>
    /// <param name="imageUrl"><see cref="ImageUrl"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public ImageBlockData(ImageBlock imageBlock, string imageUrl, int pageId) : base(imageBlock, pageId)
    {
        Initialize(imageUrl);
    }
    
    /// <summary>
    /// Create an instance of an ImageBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="imageBlock"><see cref="ContentBlock"/></param>
    /// <param name="imageUrl"><see cref="ImageUrl"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public ImageBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        ImageBlock imageBlock,
        string imageUrl, 
        int pageId
    ) : base(id, dateCreated, dateModified, imageBlock, pageId)
    {
        Initialize(imageUrl);
    }

    private void Initialize(string imageUrl)
    {
        StringLengthValidator.Validate(imageUrl, 1, 2000);
        ImageUrl = imageUrl;
    }
}
