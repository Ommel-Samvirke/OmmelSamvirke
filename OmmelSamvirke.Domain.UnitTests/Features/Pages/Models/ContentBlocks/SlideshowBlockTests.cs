using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class SlideshowBlockTests
{
    [Test]
    public void Can_Create_SlideshowBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        SlideshowBlock slideshowBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(slideshowBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(slideshowBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(slideshowBlock.Width, Is.EqualTo(width));
            Assert.That(slideshowBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_SlideshowBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        SlideshowBlock slideshowBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlock.Id, Is.EqualTo(id));
            Assert.That(slideshowBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(slideshowBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(slideshowBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(slideshowBlock.Width, Is.EqualTo(width));
            Assert.That(slideshowBlock.Height, Is.EqualTo(height));
            Assert.That(slideshowBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(slideshowBlock.DateModified, Is.EqualTo(dateModified));
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

        Assert.That(() => new SlideshowBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new SlideshowBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new SlideshowBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
