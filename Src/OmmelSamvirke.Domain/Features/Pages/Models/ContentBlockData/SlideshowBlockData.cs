using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
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

    /// <summary>
    /// Create a new instance of a SlideshowBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="slideshowBlock"><see cref="ContentBlock"/></param>
    /// <param name="imageUrls"><see cref="ImageUrls"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public SlideshowBlockData(
        SlideshowBlock slideshowBlock,
        List<Url> imageUrls,
        Page page
    ) : base(slideshowBlock, page)
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
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public SlideshowBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        SlideshowBlock slideshowBlock,
        List<Url> imageUrls,
        Page page
    ) : base(id, dateCreated, dateModified, slideshowBlock, page)
    {
        Initialize(imageUrls);
    }

    private void Initialize(List<Url> imageUrls)
    {
        NullValidator.Validate(imageUrls);
        foreach (Url imageUrl in imageUrls)
        {
            StringLengthValidator.Validate(imageUrl.Address, 5, 2000);
        }
        ImageUrls = imageUrls;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private SlideshowBlockData()
    {
        
    }
}
