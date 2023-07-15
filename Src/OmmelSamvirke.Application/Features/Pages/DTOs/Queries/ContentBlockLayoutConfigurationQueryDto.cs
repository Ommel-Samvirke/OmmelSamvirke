using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class ContentBlockLayoutConfigurationQueryDto : BaseModel
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public ContentBlockLayoutConfigurationQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        int xPosition,
        int yPosition,
        int width,
        int height
    ) : base(id, createdDate, modifiedDate)
    {
        XPosition = xPosition;
        YPosition = yPosition;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ContentBlockLayoutConfigurationQueryDto()
    {
        
    }
}
