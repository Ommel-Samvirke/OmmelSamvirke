using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

/// <summary>
/// This class represents a page template which provides a specific structure
/// and layout to a <see cref="Page"/>. The template includes a name, a list 
/// of supported layouts, associated content blocks and a state.
/// </summary>
public class PageTemplate : BaseModel
{
    /// <summary>
    /// Describes the name of the page template.
    /// Must be 1-100 characters long.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Represents the content blocks associated with this template.
    /// </summary>
    public List<ContentBlock> ContentBlocks { get; set; } = new();

    /// <summary>
    /// Describes the state of the page template.
    /// </summary>
    public PageTemplateState State { get; set; }
}
