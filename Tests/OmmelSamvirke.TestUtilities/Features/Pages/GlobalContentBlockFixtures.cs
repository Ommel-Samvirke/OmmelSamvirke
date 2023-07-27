using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Common;

namespace OmmelSamvirke.TestUtilities.Features.Pages;

public static class GlobalContentBlockFixtures
{
    public static ContentBlock DefaultContentBlock()
    {
        return EntityCreator.Create(new HeadlineBlock
        {
            Headline = "TestHeadline",
            XPosition = 0,
            YPosition = 0,
            Width = 6,
            Height = 1
        });
    }
    
    public static ContentBlockDto DefaultContentBlockDto()
    {
        return EntityCreator.Create(new HeadLineBlockDto
        {
            Headline = "TestHeadline",
            XPosition = 0,
            YPosition = 0,
            Width = 6,
            Height = 1,
            ContentBlockType = ContentBlockType.HeadlineBlock
        });
    }
}
