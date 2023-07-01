using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class ImageBlockTests
{
    [Test]
    public void Can_Create_ImageBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        ImageBlock imageBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(imageBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(imageBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(imageBlock.Width, Is.EqualTo(width));
            Assert.That(imageBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_ImageBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        ImageBlock imageBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlock.Id, Is.EqualTo(id));
            Assert.That(imageBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(imageBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(imageBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(imageBlock.Width, Is.EqualTo(width));
            Assert.That(imageBlock.Height, Is.EqualTo(height));
            Assert.That(imageBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(imageBlock.DateModified, Is.EqualTo(dateModified));
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

        Assert.That(() => new ImageBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new ImageBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new ImageBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
