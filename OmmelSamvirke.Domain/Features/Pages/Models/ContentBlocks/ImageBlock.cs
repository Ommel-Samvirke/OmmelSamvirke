namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

public class ImageBlock : ContentBlock
{
    public ImageBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
        : base(isOptional, xPosition, yPosition, width, height)
    {
    }
    
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
