using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Communities.Models;

public class Community : BaseModel
{
    /// <summary>
    /// The name of the community
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// The pages that belong to this community
    /// </summary>
    public List<Page> Pages { get; set; } = new()!;
}
