using OmmelSamvirke.Domain.Common;
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
    public T? ContentBlock { get; set; }

    /// <summary>
    /// This property is identical to <see cref="ContentBlock"/> but is of type <see cref="ContentBlock"/>.
    /// It is used to satisfy the <see cref="IContentBlockData"/> interface.
    /// Without this property it is not possible to create collections with mixed types of <see cref="ContentBlockData{T}"/>.
    /// </summary>
    public ContentBlock? BaseContentBlock => ContentBlock;

    /// <summary>
    /// The page that this content block data is associated with.
    /// </summary>
    public Page? Page { get; set; }
}
