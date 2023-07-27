using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalPageFixtures
{
    public static Page DefaultPage()
    {
        return EntityCreator.Create(new Page
        {
            Name = "TestPage",
            State = PageState.Hidden,
            CommunityId = 1,
            DesktopConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfiguration(), 
            TabletConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfiguration(), 
            MobileConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfiguration()
        });
    }
    
    public static PageDto DefaultPageDto()
    {
        return EntityCreator.Create(new PageDto
        {
            Name = "TestPage",
            State = PageState.Hidden,
            CommunityId = 1,
            DesktopConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfigurationDto(), 
            TabletConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfigurationDto(), 
            MobileConfiguration = GlobalLayoutConfigurationFixtures.DefaultContentBlockLayoutConfigurationDto()
        });
    }
}
