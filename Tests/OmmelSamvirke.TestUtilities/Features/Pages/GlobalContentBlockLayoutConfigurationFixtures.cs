using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalContentBlockLayoutConfigurationFixtures
{
    public static ContentBlockLayoutConfiguration GetDefaultContentBlockLayoutConfiguration()
    {
        return EntityCreator.Create(new ContentBlockLayoutConfiguration
        {
            Width = 6,
            Height = 3,
            XPosition = 0,
            YPosition = 0
        });
    }
    
    public static ContentBlockLayoutConfiguration GetDefaultContentBlockLayoutConfiguration(
        int width,
        int height,
        int xPosition,
        int yPosition
    )
    {
        return EntityCreator.Create(new ContentBlockLayoutConfiguration
        {
            Width = width,
            Height = height,
            XPosition = xPosition,
            YPosition = yPosition
        });
    }
}
