using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
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
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Represents a list of layouts that are supported by this template.
    /// </summary>
    public ISet<Layouts> SupportedLayouts { get; private set; } = new HashSet<Layouts>();

    /// <summary>
    /// Represents the content blocks associated with this template.
    /// </summary>
    public List<ContentBlock> Blocks { get; private set; } = new();

    /// <summary>
    /// Describes the state of the page template.
    /// </summary>
    public PageTemplateState State { get; set; }

    /// <summary>
    /// Create a new instance of a page template.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="supportedLayouts"><see cref="SupportedLayouts"/></param>
    /// <param name="contentBlocks"><see cref="Blocks"/></param>
    /// <param name="state"><see cref="State"/></param>
    public PageTemplate(string name, ISet<Layouts> supportedLayouts, List<ContentBlock> contentBlocks, PageTemplateState state)
    {
        Initialize(name, supportedLayouts, contentBlocks, state);
    }

    /// <summary>
    /// Create an instance of a page template that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="supportedLayouts"><see cref="SupportedLayouts"/></param>
    /// <param name="contentBlocks"><see cref="Blocks"/></param>
    /// <param name="state"><see cref="State"/></param>
    public PageTemplate(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name,
        ISet<Layouts> supportedLayouts,
        List<ContentBlock> contentBlocks,
        PageTemplateState state
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(name, supportedLayouts, contentBlocks, state);
    }

    /// <summary>
    /// Initializes the PageTemplate object after validation.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="supportedLayouts"><see cref="SupportedLayouts"/></param>
    /// <param name="contentBlocks"><see cref="Blocks"/></param>
    /// <param name="state"><see cref="State"/></param>
    private void Initialize(string name, ISet<Layouts> supportedLayouts, List<ContentBlock> contentBlocks, PageTemplateState state)
    {
        StringLengthValidator.Validate(name, 1, 100);
        NullValidator.Validate(supportedLayouts);
        NullValidator.Validate(contentBlocks);
        
        Name = name;
        SupportedLayouts = supportedLayouts;
        Blocks = contentBlocks;
        State = state;
    }
}
