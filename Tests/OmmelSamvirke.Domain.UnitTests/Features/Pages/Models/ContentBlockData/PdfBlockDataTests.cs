using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class PdfBlockDataTests : PagesBaseTestModel
{
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_PdfBlockData_With_Valid_Data()
    {
        const int pageId = 1;
        const string pdfUrl = "https://example.com/somepdf.pdf";
        PdfBlockData pdfBlockData = new(DefaultPdfBlock, pdfUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(pdfBlockData.ContentBlock, Is.EqualTo(DefaultPdfBlock));
            Assert.That(pdfBlockData.PdfUrl, Is.EqualTo(pdfUrl));
            Assert.That(pdfBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_PdfBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        const string pdfUrl = "https://example.com/somepdf.pdf";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        PdfBlockData pdfBlockData = new(id, dateCreated, dateModified, DefaultPdfBlock, pdfUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(pdfBlockData.Id, Is.EqualTo(id));
            Assert.That(pdfBlockData.ContentBlock, Is.EqualTo(DefaultPdfBlock));
            Assert.That(pdfBlockData.PdfUrl, Is.EqualTo(pdfUrl));
            Assert.That(pdfBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(pdfBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(pdfBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        const string pdfUrl = "https://example.com/somepdf.pdf";
        PdfBlock nullContentBlock = null!;

        Assert.That(() => new PdfBlockData(nullContentBlock, pdfUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_PdfUrl_Is_Empty()
    {
        const int pageId = 1;
        const string pdfUrl = "";

        Assert.That(() => new PdfBlockData(DefaultPdfBlock, pdfUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_PdfUrl_Is_Too_Long()
    {
        const int pageId = 1;
        string pdfUrl = new('a', 2001);

        Assert.That(() => new PdfBlockData(DefaultPdfBlock, pdfUrl, pageId), Throws.ArgumentException);
    }
}
