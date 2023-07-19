using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages;

[TestFixture]
public class ContentBlockTests
{
    private static List<ContentBlock> _contentBlocks = null!;

    [Test]
    public void AreAnyBlocksOverlapping_WhenNoBlocks_ReturnsFalse()
    {
        _contentBlocks = new List<ContentBlock>();

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.False);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenOneBlock_ReturnsFalse()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock()
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.False);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenTwoBlocksAtSameLocation_ReturnsTrue()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(),
            GlobalContentBlockFixtures.DefaultContentBlock()
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.True);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenLeftTouchesRight_ReturnFalse()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0)
            ),
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 6, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 6, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 6, 0)
            )
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.False);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenLeftOverlapsRight_ReturnTrue()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0)
            ),
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 5, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 5, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 5, 0)
            )
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.True);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenTopTouchesBottom_ReturnFalse()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0)
            ),
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 3),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 3),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 3)
            )
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.False);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenTopOverlapsBottom_ReturnTrue()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 0)
            ),
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 2),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 2),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 3, 0, 2)
            )
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.True);
    }
    
    [Test]
    public void AreAnyBlocksOverlapping_WhenBlockIsSupersetOfOtherBlock_ReturnTrue()
    {
        _contentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(1, 1, 2, 2),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(1, 1, 2, 2),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(1, 1, 2, 2)
            ),
            GlobalContentBlockFixtures.DefaultContentBlock(
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 6, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 6, 0, 0),
                GlobalContentBlockLayoutConfigurationFixtures.GetDefaultContentBlockLayoutConfiguration(6, 6, 0, 0)
            )
        };

        Assert.That(ContentBlock.AreAnyBlocksOverlapping(_contentBlocks), Is.True);
    }
}
