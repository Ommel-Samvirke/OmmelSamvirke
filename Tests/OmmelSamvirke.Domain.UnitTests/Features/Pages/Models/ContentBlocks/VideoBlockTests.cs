using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class VideoBlockTests : PagesBaseTestModel
{
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }
    
    [Test]
    public void Can_Create_VideoBlock_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DefaultVideoBlock.IsOptional, Is.EqualTo(false));
            Assert.That(DefaultVideoBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultVideoBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultVideoBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
        });
    }

    [Test]
    public void Can_Create_VideoBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        DateTime now = DateTime.Now;

        VideoBlock videoBlock = new(
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
            Assert.That(videoBlock.Id, Is.EqualTo(id));
            Assert.That(videoBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(DefaultVideoBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultVideoBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultVideoBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
            Assert.That(videoBlock.DateCreated, Is.EqualTo(now));
            Assert.That(videoBlock.DateModified, Is.EqualTo(now));
        });
    }
}
