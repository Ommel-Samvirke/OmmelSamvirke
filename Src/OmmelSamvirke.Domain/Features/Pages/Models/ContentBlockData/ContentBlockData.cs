using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This abstract class represents a piece of data for a specific type of 
/// <see cref="ContentBlock"/>. It is intended to be extended for various
/// types of content blocks.
/// </summary>
public abstract class ContentBlockData<T> : BaseModel, IContentBlockData where T : ContentBlock
{
    /// <summary>
    /// Represents the content block that this data is associated with.
    /// </summary>
    public T ContentBlock { get; set; } = null!;

    /// <summary>
    /// This property is identical to <see cref="ContentBlock"/> but is of type <see cref="ContentBlock"/>.
    /// It is used to satisfy the <see cref="IContentBlockData"/> interface.
    /// Without this property it is not possible to create collections with mixed types of <see cref="ContentBlockData{T}"/>.
    /// </summary>
    public ContentBlock BaseContentBlock => ContentBlock;

    /// <summary>
    /// The page that this content block data is associated with.
    /// </summary>
    public Page Page { get; set; } = null!;

    /// <summary>
    /// Create a new instance of a content block data.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="contentBlock"><see cref="ContentBlock"/></param>
    /// <param name="page"><see cref="Page"/></param>
    protected ContentBlockData(T contentBlock, Page page)
    {
        Initialize(contentBlock, page);
    }
    
    /// <summary>
    /// Create an instance of a content block data that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="contentBlock"><see cref="ContentBlock"/></param>
    /// <param name="page"><see cref="Page"/></param>
    protected ContentBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        T contentBlock,
        Page page
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(contentBlock, page);
    }

    private void Initialize(T contentBlock, Page page)
    {
        NullValidator.Validate(contentBlock);
        NullValidator.Validate(page);
        
        ContentBlock = contentBlock;
        Page = page;
    }
    
    /// <summary>
    /// Only for EF Core private constructors.
    /// </summary>
    protected ContentBlockData()
    { }
}
