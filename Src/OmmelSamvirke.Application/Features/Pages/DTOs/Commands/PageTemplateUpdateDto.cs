using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageTemplateUpdateDto : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public List<ContentBlockCreateDto> ContentBlocks { get; set; } = new();
    public PageTemplateState State { get; set;  }
}
