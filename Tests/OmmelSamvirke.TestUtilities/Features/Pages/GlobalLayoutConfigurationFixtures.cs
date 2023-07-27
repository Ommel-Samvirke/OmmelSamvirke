using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalLayoutConfigurationFixtures
{
    public static LayoutConfiguration DefaultContentBlockLayoutConfiguration()
    {
        return EntityCreator.Create(new LayoutConfiguration
        {
            ContentBlocks = new List<ContentBlock>
            {
                GlobalContentBlockFixtures.DefaultContentBlock(),
            }
        });
    }
    
    public static LayoutConfigurationDto DefaultContentBlockLayoutConfigurationDto()
    {
        return EntityCreator.Create(new LayoutConfigurationDto
        {
            ContentBlocks = new List<ContentBlockDto>
            {
                GlobalContentBlockFixtures.DefaultContentBlockDto(),
            }
        });
    }
}
