using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageTemplateWithoutContentBlocksQueryDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public PageTemplateState PageTemplateState { get; set; }
}
