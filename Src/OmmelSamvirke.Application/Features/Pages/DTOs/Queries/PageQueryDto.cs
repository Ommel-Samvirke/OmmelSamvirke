using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageQueryDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
}
