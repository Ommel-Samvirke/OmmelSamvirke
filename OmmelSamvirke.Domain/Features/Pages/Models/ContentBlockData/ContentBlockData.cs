using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This abstract class represents a piece of data for a specific type of 
/// <see cref="ContentBlock"/>. It is intended to be extended for various
/// types of content blocks.
/// </summary>
public abstract class ContentBlockData<T>: BaseModel where T : ContentBlock
{
    /// <summary>
    /// Represents the content block that this data is associated with.
    /// </summary>
    public T ContentBlock { get; private set; } = null!;
    
    /// <summary>
    /// The ID of the page that this content block data is associated with.
    /// </summary>
    public int PageId { get; private set; }

    /// <summary>
    /// Create a new instance of a content block data.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="contentBlock"><see cref="ContentBlock"/></param>
    /// <param name="pageId"><see cref="PageId"/></param>
    protected ContentBlockData(T contentBlock, int pageId)
    {
        Initialize(contentBlock, pageId);
    }
    
    /// <summary>
    /// Create an instance of a content block data that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="contentBlock"><see cref="ContentBlock"/></param>
    /// <param name="pageId"><see cref="PageId"/></param>
    protected ContentBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        T contentBlock,
        int pageId
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(contentBlock, pageId);
    }

    private void Initialize(T contentBlock, int pageId)
    {
        NullValidator.Validate(contentBlock);
        IntegerValidator.Validate(pageId, 0);
        
        ContentBlock = contentBlock;
        PageId = pageId;
    }
}
