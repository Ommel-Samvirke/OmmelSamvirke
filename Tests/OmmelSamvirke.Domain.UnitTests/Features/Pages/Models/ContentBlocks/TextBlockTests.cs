using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class TextBlockTests : PagesBaseTestModel
{
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }
    
    [Test]
    public void Can_Create_TextBlock_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DefaultTextBlock.IsOptional, Is.EqualTo(false));
            Assert.That(DefaultTextBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultTextBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultTextBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
        });
    }

    [Test]
    public void Can_Create_TextBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        DateTime now = DateTime.Now;

        TextBlock textBlock = new(
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
            Assert.That(textBlock.Id, Is.EqualTo(id));
            Assert.That(textBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(DefaultTextBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultTextBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultTextBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
            Assert.That(textBlock.DateCreated, Is.EqualTo(now));
            Assert.That(textBlock.DateModified, Is.EqualTo(now));
        });
    }
}
