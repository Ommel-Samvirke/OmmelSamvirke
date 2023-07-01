using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
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
    public string Headline { get; private set; } = null!;

    /// <summary>
    /// Create a new instance of a HeadlineBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="headlineBlock"><see cref="ContentBlock"/></param>
    /// <param name="headline"><see cref="Headline"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public HeadlineBlockData(HeadlineBlock headlineBlock, string headline, int pageId) : base(headlineBlock, pageId)
    {
        Initialize(headline);
    }

    /// <summary>
    /// Create an instance of a HeadlineBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="headlineBlock"><see cref="ContentBlock"/></param>
    /// <param name="headline"><see cref="Headline"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public HeadlineBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        HeadlineBlock headlineBlock,
        string headline, 
        int pageId
    ) : base(id, dateCreated, dateModified, headlineBlock, pageId)
    {
        Initialize(headline);
    }
    
    private void Initialize(string headline)
    {
        StringLengthValidator.Validate(headline, 1, 200);
        Headline = headline;
    }
}
