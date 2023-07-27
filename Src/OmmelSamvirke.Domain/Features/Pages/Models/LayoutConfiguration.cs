using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

public class LayoutConfiguration : BaseModel
{
    /// <summary>
    /// Represents the content blocks associated with this page.
    /// </summary>
    public List<ContentBlock> ContentBlocks { get; set; } = new();
}
