using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class TextBlockTests
{
    [Test]
    public void Can_Create_TextBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        TextBlock textBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(textBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(textBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(textBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(textBlock.Width, Is.EqualTo(width));
            Assert.That(textBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_TextBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        TextBlock textBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(textBlock.Id, Is.EqualTo(id));
            Assert.That(textBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(textBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(textBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(textBlock.Width, Is.EqualTo(width));
            Assert.That(textBlock.Height, Is.EqualTo(height));
            Assert.That(textBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(textBlock.DateModified, Is.EqualTo(dateModified));
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

        Assert.That(() => new TextBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new TextBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new TextBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
