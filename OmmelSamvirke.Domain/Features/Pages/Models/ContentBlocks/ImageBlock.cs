using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents an image block that can be placed on a page. 
/// It extends the abstract ContentBlock class.
/// </summary>
public class ImageBlock : ContentBlock
{
    /// <summary>
    /// Create a new instance of an ImageBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="isOptional"><see cref="ContentBlock.IsOptional"/></param>
    /// <param name="xPosition"><see cref="ContentBlock.XPosition"/></param>
    /// <param name="yPosition"><see cref="ContentBlock.YPosition"/></param>
    /// <param name="width"><see cref="ContentBlock.Width"/></param>
    /// <param name="height"><see cref="ContentBlock.Height"/></param>
    public ImageBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
        : base(isOptional, xPosition, yPosition, width, height)
    {
    }
    
    /// <summary>
    /// Create an instance of an ImageBlock that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="isOptional"><see cref="ContentBlock.IsOptional"/></param>
    /// <param name="xPosition"><see cref="ContentBlock.XPosition"/></param>
    /// <param name="yPosition"><see cref="ContentBlock.YPosition"/></param>
    /// <param name="width"><see cref="ContentBlock.Width"/></param>
    /// <param name="height"><see cref="ContentBlock.Height"/></param>
    public ImageBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        int xPosition,
        int yPosition,
        int width,
        int? height
    ) : base(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height)
    {
    }
}
