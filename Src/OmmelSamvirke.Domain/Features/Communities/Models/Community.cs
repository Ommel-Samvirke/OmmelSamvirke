using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Communities.Models;

public class Community : BaseModel
{
    /// <summary>
    /// The name of the community
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The pages that belong to this community
    /// </summary>
    public List<Page> Pages { get; set; }


    /// <summary>
    /// Create a new instance of a page.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="pages"><see cref="Pages"/></param>
    public Community(string name, List<Page> pages)
    {
        Initialize(name, pages);
    }

    /// <summary>
    /// Create an instance of a page that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="pages"><see cref="Pages"/></param>
    public Community(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name,
        List<Page> pages) : base(id, dateCreated, dateModified)
    {
        Initialize(name, pages);
    }

    private void Initialize(string name, List<Page> pages)
    {
        StringLengthValidator.Validate(name, 1, 100);
        NullValidator.Validate(pages);
        Name = name;
        Pages = pages;
    }
}
