using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalContentBlockDataFixtures
{
    public static HeadlineBlockData DefaultContentBlockData()
    {
        return EntityCreator.Create(new HeadlineBlockData
        {
            Headline = "TestHeadline",
            ContentBlock = (HeadlineBlock)GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration()
            ),
            PageId = 1,
            ContentBlockId = 1
        });
    }
}
