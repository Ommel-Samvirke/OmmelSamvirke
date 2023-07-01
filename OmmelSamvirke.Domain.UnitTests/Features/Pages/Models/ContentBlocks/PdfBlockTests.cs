using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class PdfBlockTests
{
    [Test]
    public void Can_Create_PdfBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        PdfBlock pdfBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(pdfBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(pdfBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(pdfBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(pdfBlock.Width, Is.EqualTo(width));
            Assert.That(pdfBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_PdfBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        PdfBlock pdfBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(pdfBlock.Id, Is.EqualTo(id));
            Assert.That(pdfBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(pdfBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(pdfBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(pdfBlock.Width, Is.EqualTo(width));
            Assert.That(pdfBlock.Height, Is.EqualTo(height));
            Assert.That(pdfBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(pdfBlock.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_XPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = -1;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new PdfBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new PdfBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new PdfBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
