using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class ContentBlockLayoutConfigurationQueryDto : BaseModel
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; } = 1;
    public int Height { get; set; } = 1;
}
