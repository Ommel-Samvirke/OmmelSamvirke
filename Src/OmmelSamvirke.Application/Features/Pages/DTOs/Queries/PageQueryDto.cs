using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageQueryDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public PageState State { get; set; }
}
