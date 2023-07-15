using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a content block that can be placed on a page. 
/// It serves as a base class for more specific types of content blocks.
/// </summary>
public abstract class ContentBlock : BaseModel
{
    /// <summary>
    /// Whether the block is optional or not.
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// The layout configuration for Desktop devices.
    /// </summary>
    public ContentBlockLayoutConfiguration DesktopConfiguration { get; set; } = null!;

    /// <summary>
    /// The layout configuration for Tablet devices.
    /// </summary>
    public ContentBlockLayoutConfiguration TabletConfiguration { get; set; } = null!;

    /// <summary>
    /// The layout configuration for Mobile devices.
    /// </summary>
    public ContentBlockLayoutConfiguration MobileConfiguration { get; set; } = null!;

    /// <summary>
    /// The page template that this content block is associated with.
    /// </summary>
    public PageTemplate PageTemplate { get; set; } = null!;

    /// <summary>
    /// Create a new instance of a ContentBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="isOptional"><see cref="IsOptional"/></param>
    /// <param name="desktopConfiguration"><see cref="DesktopConfiguration"/></param>
    /// <param name="tabletConfiguration"><see cref="TabletConfiguration"/></param>
    /// <param name="mobileConfiguration"><see cref="MobileConfiguration"/></param>
    /// <param name="pageTemplate"><see cref="PageTemplate"/></param>
    protected ContentBlock(
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration,
        PageTemplate pageTemplate
    )
    {
        Initialize(isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration, pageTemplate);
    }

    /// <summary>
    /// Create an instance of a ContentBlock that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="isOptional"><see cref="IsOptional"/></param>
    /// <param name="desktopConfiguration"><see cref="DesktopConfiguration"/></param>
    /// <param name="tabletConfiguration"><see cref="TabletConfiguration"/></param>
    /// <param name="mobileConfiguration"><see cref="MobileConfiguration"/></param>
    /// <param name="pageTemplate"><see cref="PageTemplate"/></param>
    protected ContentBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration,
        PageTemplate pageTemplate
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration, pageTemplate);
    }

    private void Initialize(
        bool isOptional, 
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration,
        PageTemplate pageTemplate
    )
    {
        IsOptional = isOptional;
        DesktopConfiguration = desktopConfiguration;
        TabletConfiguration = tabletConfiguration;
        MobileConfiguration = mobileConfiguration;
        PageTemplate = pageTemplate;
    }
    
    /// <summary>
    /// Only for EF Core private constructors.
    /// </summary>
    protected ContentBlock()
    {
        
    }
}
