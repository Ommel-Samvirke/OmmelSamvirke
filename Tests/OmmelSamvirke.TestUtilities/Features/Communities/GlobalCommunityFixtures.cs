using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Communities;

public static class GlobalCommunityFixtures
{
    public static Community DefaultCommunity()
    {
        return EntityCreator.Create(new Community
        {
            Name = "TestCommunity",
            Pages = new List<Page>()
        });
    }
}
