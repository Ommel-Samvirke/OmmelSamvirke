using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class SlideshowBlockTests : PagesBaseTestModel
{
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }
    
    [Test]
    public void Can_Create_SlideshowBlock_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DefaultSlideshowBlock.IsOptional, Is.EqualTo(false));
            Assert.That(DefaultSlideshowBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultSlideshowBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultSlideshowBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
        });
    }

    [Test]
    public void Can_Create_SlideshowBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        DateTime now = DateTime.Now;

        SlideshowBlock slideshowBlock = new(
            id,
            now,
            now,
            isOptional,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlock.Id, Is.EqualTo(id));
            Assert.That(slideshowBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(DefaultSlideshowBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultSlideshowBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultSlideshowBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
            Assert.That(slideshowBlock.DateCreated, Is.EqualTo(now));
            Assert.That(slideshowBlock.DateModified, Is.EqualTo(now));
        });
    }
}
