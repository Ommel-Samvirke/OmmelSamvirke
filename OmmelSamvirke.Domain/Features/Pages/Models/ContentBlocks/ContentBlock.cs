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
    public bool IsOptional { get; private set; }

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
    /// Create a new instance of a ContentBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="isOptional"><see cref="IsOptional"/></param>
    /// <param name="desktopConfiguration"><see cref="DesktopConfiguration"/></param>
    /// <param name="tabletConfiguration"><see cref="TabletConfiguration"/></param>
    /// <param name="mobileConfiguration"><see cref="MobileConfiguration"/></param>
    protected ContentBlock(
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration
    )
    {
        Initialize(isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration);
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
    protected ContentBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration);
    }

    private void Initialize(
        bool isOptional, 
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration
    )
    {
        IsOptional = isOptional;
        DesktopConfiguration = desktopConfiguration;
        TabletConfiguration = tabletConfiguration;
        MobileConfiguration = mobileConfiguration;
    }
}
