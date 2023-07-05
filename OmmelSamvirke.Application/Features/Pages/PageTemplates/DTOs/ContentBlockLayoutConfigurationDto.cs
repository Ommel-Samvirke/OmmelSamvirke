namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class ContentBlockLayoutConfigurationDto
{
    public int Id { get; set; }
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public ContentBlockLayoutConfigurationDto(
        int id,
        int xPosition,
        int yPosition,
        int width,
        int height
    )
    {
        Id = id;
        XPosition = xPosition;
        YPosition = yPosition;
        Width = width;
        Height = height;
    }
}
