using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public class GlobalPageFixtures
{
    public static Page DefaultPage()
    {
        Random random = new();
        
        return EntityCreator.Create(new Page
        {
            Name = $"TestPage {random.NextInt64(100_000_000)}",
            State = PageState.Hidden,
            CommunityId = 1,
            TemplateId = 1
        });
    }
}
