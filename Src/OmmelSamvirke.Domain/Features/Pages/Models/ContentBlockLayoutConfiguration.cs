using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

public class ContentBlockLayoutConfiguration : BaseModel
{
    /// <summary>
    /// The X position of the block on the page.
    /// </summary>
    public int XPosition { get; set; }

    /// <summary>
    /// The Y position of the block on the page.
    /// </summary>
    public int YPosition { get; set; }

    /// <summary>
    /// The width of the block.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the block.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Create an instance of a ContentBlock that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="xPosition"><see cref="XPosition"/></param>
    /// <param name="yPosition"><see cref="YPosition"/></param>
    /// <param name="width"><see cref="Width"/></param>
    /// <param name="height"><see cref="Height"/></param>
    public ContentBlockLayoutConfiguration(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        int xPosition,
        int yPosition,
        int width,
        int height = 1
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(xPosition, yPosition, width, height);
    }
    
    /// <summary>
    /// Create a new instance of a ContentBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="xPosition"><see cref="XPosition"/></param>
    /// <param name="yPosition"><see cref="YPosition"/></param>
    /// <param name="width"><see cref="Width"/></param>
    /// <param name="height"><see cref="Height"/></param>
    public ContentBlockLayoutConfiguration(
        int xPosition,
        int yPosition,
        int width,
        int height = 1    
    )
    {
        Initialize(xPosition, yPosition, width, height);
    }
    
    private void Initialize(int xPosition, int yPosition, int width, int height)
    {
        IntegerValidator.Validate(xPosition, 0);
        IntegerValidator.Validate(yPosition, 0);
        IntegerValidator.Validate(width, 1);
        IntegerValidator.Validate(height, 1);
        XPosition = xPosition;
        YPosition = yPosition;
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private ContentBlockLayoutConfiguration()
    {
        
    }
}
