using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

public abstract class ContentBlock : BaseModel
{
    public bool IsOptional { get; private set; }
    public int XPosition { get; private set; }
    public int YPosition { get; private set; }
    public int Width { get; private set; }
    public int? Height { get; private set; }

    public ContentBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
    {
        Initialize(isOptional, xPosition, yPosition, width, height);
    }
    
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
