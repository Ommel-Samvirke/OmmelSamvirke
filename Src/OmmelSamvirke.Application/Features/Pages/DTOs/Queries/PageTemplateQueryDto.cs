using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageTemplateQueryDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public List<ContentBlockQueryDto> ContentBlocks { get; set; } = new();
    public PageTemplateState State { get; set; }
}
