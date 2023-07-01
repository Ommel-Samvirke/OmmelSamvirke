using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

public class VideoBlockTests
{
    [Test]
    public void Can_Create_VideoBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        VideoBlock videoBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(videoBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(videoBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(videoBlock.Width, Is.EqualTo(width));
            Assert.That(videoBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_VideoBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        VideoBlock videoBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlock.Id, Is.EqualTo(id));
            Assert.That(videoBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(videoBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(videoBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(videoBlock.Width, Is.EqualTo(width));
            Assert.That(videoBlock.Height, Is.EqualTo(height));
            Assert.That(videoBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(videoBlock.DateModified, Is.EqualTo(dateModified));
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

        Assert.That(() => new VideoBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new VideoBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new VideoBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
