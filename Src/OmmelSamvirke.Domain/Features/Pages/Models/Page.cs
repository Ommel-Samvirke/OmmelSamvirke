using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

/// <summary>
/// This class represents a page which is created based on a certain 
/// <see cref="PageTemplate"/>. Each page must have a unique name 
/// and it is associated with a specific template.
/// </summary>
public class Page : BaseModel
{
    /// <summary>
    /// Describes the name of the page.
    /// Must be 1-100 characters long.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The Id of the template which is used to create this page.
    /// </summary>
    public int TemplateId { get; set; }

    /// <summary>
    /// The id of the community the page belongs to.
    /// </summary>
    public int CommunityId { get; set; }
}
