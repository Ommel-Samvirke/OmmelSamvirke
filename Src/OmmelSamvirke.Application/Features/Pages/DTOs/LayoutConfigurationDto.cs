using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;
using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs;

public class LayoutConfigurationDto : BaseModel
{
    public List<ContentBlockDto> ContentBlocks { get; set; } = new();
}
