using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class ImageBlockTests : PagesBaseTestModel
{
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }
    
    [Test]
    public void Can_Create_ImageBlock_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DefaultImageBlock.IsOptional, Is.EqualTo(false));
            Assert.That(DefaultImageBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultImageBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultImageBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
        });
    }

    [Test]
    public void Can_Create_ImageBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        DateTime now = DateTime.Now;

        ImageBlock imageBlock = new(
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
            Assert.That(imageBlock.Id, Is.EqualTo(id));
            Assert.That(imageBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(DefaultImageBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultImageBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultImageBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
            Assert.That(imageBlock.DateCreated, Is.EqualTo(now));
            Assert.That(imageBlock.DateModified, Is.EqualTo(now));
        });
    }
}
