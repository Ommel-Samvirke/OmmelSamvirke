using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

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
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Represents the template which is used to create this page.
    /// </summary>
    public PageTemplate Template { get; private set; } = null!;

    /// <summary>
    /// Create a new instance of a page.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="template"><see cref="Template"/></param>
    public Page(string name, PageTemplate template)
    {
        Initialize(name, template);
    }
    
    /// <summary>
    /// Create an instance of a page that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="template"><see cref="Template"/></param>
    public Page(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name,
        PageTemplate template
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(name, template);
    }

    private void Initialize(string name, PageTemplate template)
    {
        StringLengthValidator.Validate(name, 1, 100);
        NullValidator.Validate(template);
        Name = name;
        Template = template;
    }
}
