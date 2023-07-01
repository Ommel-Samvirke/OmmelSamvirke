using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a content block that can be placed on a page. 
/// It serves as a base class for more specific types of content blocks.
/// </summary>
public abstract class ContentBlock : BaseModel
{
    /// <summary>
    /// Whether the block is optional or not.
    /// </summary>
    public bool IsOptional { get; private set; }

    /// <summary>
    /// The X position of the block on the page.
    /// </summary>
    public int XPosition { get; private set; }

    /// <summary>
    /// The Y position of the block on the page.
    /// </summary>
    public int YPosition { get; private set; }

    /// <summary>
    /// The width of the block.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// The height of the block.
    /// </summary>
    public int? Height { get; private set; }

    /// <summary>
    /// Create a new instance of a ContentBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="isOptional"><see cref="IsOptional"/></param>
    /// <param name="xPosition"><see cref="XPosition"/></param>
    /// <param name="yPosition"><see cref="YPosition"/></param>
    /// <param name="width"><see cref="Width"/></param>
    /// <param name="height"><see cref="Height"/></param>
    public ContentBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
    {
        Initialize(isOptional, xPosition, yPosition, width, height);
    }
    
    /// <summary>
    /// Create an instance of a ContentBlock that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="isOptional"><see cref="IsOptional"/></param>
    /// <param name="xPosition"><see cref="XPosition"/></param>
    /// <param name="yPosition"><see cref="YPosition"/></param>
    /// <param name="width"><see cref="Width"/></param>
    /// <param name="height"><see cref="Height"/></param>
    public ContentBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        int xPosition,
        int yPosition,
        int width,
        int? height
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(isOptional, xPosition, yPosition, width, height);
    }

    private void Initialize(bool isOptional, int xPosition, int yPosition, int width, int? height)
    {
        IsOptional = isOptional;
        IntegerValidator.Validate(xPosition, 0);
        IntegerValidator.Validate(yPosition, 0);
        IntegerValidator.Validate(width, 1);
        XPosition = xPosition;
        YPosition = yPosition;
        Width = width;
        Height = height;
    }
}
