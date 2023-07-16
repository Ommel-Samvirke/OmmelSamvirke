using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public abstract class ContentBlockDataQueryDto : BaseModel
{
    protected ContentBlockQueryDto ContentBlock { get; set; } = new();
    protected PageQueryDto Page { get; set; } = new();
}
