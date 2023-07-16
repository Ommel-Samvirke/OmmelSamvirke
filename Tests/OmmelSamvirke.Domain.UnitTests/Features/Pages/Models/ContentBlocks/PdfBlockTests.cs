using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class PdfBlockTests : PagesBaseTestModel
{
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_PdfBlock_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DefaultPdfBlock.IsOptional, Is.EqualTo(false));
            Assert.That(DefaultPdfBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultPdfBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultPdfBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
        });
    }

    [Test]
    public void Can_Create_PdfBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        DateTime now = DateTime.UtcNow;

        PdfBlock pdfBlock = new(
            id,
            now,
            now,
            isOptional,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );

        Assert.Multiple(() =>
        {
            Assert.That(pdfBlock.Id, Is.EqualTo(id));
            Assert.That(pdfBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(DefaultPdfBlock.DesktopConfiguration, Is.EqualTo(DefaultDesktopConfiguration));
            Assert.That(DefaultPdfBlock.TabletConfiguration, Is.EqualTo(DefaultTabletConfiguration));
            Assert.That(DefaultPdfBlock.MobileConfiguration, Is.EqualTo(DefaultMobileConfiguration));
            Assert.That(pdfBlock.DateCreated, Is.EqualTo(now));
            Assert.That(pdfBlock.DateModified, Is.EqualTo(now));
        });
    }
}
