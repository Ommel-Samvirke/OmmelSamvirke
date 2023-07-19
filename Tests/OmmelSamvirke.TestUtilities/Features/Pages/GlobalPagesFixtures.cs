using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalPagesFixtures
{
    public static PageTemplate DefaultPageTemplate()
    {
        return EntityCreator.Create(new PageTemplate
        {
            Name = "TestPageTemplate",
            State = PageTemplateState.Public,
        });
    }
}
