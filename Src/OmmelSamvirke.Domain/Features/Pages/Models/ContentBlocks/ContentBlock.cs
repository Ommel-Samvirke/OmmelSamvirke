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
    public ContentBlockLayoutConfiguration? DesktopConfiguration { get; set; }

    /// <summary>
    /// The layout configuration for Tablet devices.
    /// </summary>
    public ContentBlockLayoutConfiguration? TabletConfiguration { get; set; }

    /// <summary>
    /// The layout configuration for Mobile devices.
    /// </summary>
    public ContentBlockLayoutConfiguration? MobileConfiguration { get; set; }
    
    /// <summary>
    /// Check if any of the blocks in <paramref name="contentBlocks"/> are overlapping.
    /// The check is performed for all three layout configurations.
    /// </summary>
    /// <param name="contentBlocks"></param>
    /// <returns></returns>
    public static bool AreAnyBlocksOverlapping(List<ContentBlock> contentBlocks)
    {
        return CheckLayoutConfiguration(contentBlocks, cb => cb.DesktopConfiguration) ||
               CheckLayoutConfiguration(contentBlocks, cb => cb.TabletConfiguration) ||
               CheckLayoutConfiguration(contentBlocks, cb => cb.MobileConfiguration);
    }

    private static bool CheckLayoutConfiguration(
        IEnumerable<ContentBlock> contentBlocks,
        Func<ContentBlock, ContentBlockLayoutConfiguration?> configSelector
    )
    {
        List<ContentBlockLayoutConfiguration?> configs = contentBlocks
            .Select(configSelector)
            .Where(config => config != null)
            .ToList();

        for (int i = 0; i < configs.Count; i++)
        {
            for (int j = i + 1; j < configs.Count; j++)
            {
                if (IsOverlapping(configs[i], configs[j]))
                    return true;
            }
        }

        return false;
    }

    private static bool IsOverlapping(
        ContentBlockLayoutConfiguration? contentBlock1Config,
        ContentBlockLayoutConfiguration? contentBlock2Config
    )
    {
        if (contentBlock1Config == null || contentBlock2Config == null) 
            return false;
        
        if (contentBlock1Config.XPosition + contentBlock1Config.Width <= contentBlock2Config.XPosition || 
            contentBlock2Config.XPosition + contentBlock2Config.Width <= contentBlock1Config.XPosition)
            return false;
        
        if (contentBlock1Config.YPosition + contentBlock1Config.Height > contentBlock2Config.YPosition || 
               contentBlock2Config.YPosition + contentBlock2Config.Height > contentBlock1Config.YPosition)
            return false;

        return true;
    }
}
