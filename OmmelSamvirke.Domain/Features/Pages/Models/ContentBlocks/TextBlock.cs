namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

public class TextBlock : ContentBlock
{
    public TextBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
        : base(isOptional, xPosition, yPosition, width, height)
    {
    }
    
    public TextBlock(
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
