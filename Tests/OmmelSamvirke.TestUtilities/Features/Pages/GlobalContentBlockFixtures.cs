using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalContentBlockFixtures
{
    public static ContentBlock DefaultContentBlock()
    {
        return EntityCreator.Create(new HeadlineBlock
        {
            IsOptional = false,
            DesktopConfiguration = GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(),
            TabletConfiguration = GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(),
            MobileConfiguration = GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration()
        });
    }
    
    public static ContentBlock DefaultContentBlock(
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration
    )
    {
        return EntityCreator.Create(new HeadlineBlock
        {
            IsOptional = false,
            DesktopConfiguration = desktopConfiguration,
            TabletConfiguration = tabletConfiguration,
            MobileConfiguration = mobileConfiguration
        });
    }
}