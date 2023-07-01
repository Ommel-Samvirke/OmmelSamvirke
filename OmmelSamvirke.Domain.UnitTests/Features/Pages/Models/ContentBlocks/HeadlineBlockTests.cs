using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

[TestFixture]
public class HeadlineBlockTests
{
    [Test]
    public void Can_Create_HeadlineBlock_With_Valid_Data()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        HeadlineBlock headlineBlock = new(isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(headlineBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(headlineBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(headlineBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(headlineBlock.Width, Is.EqualTo(width));
            Assert.That(headlineBlock.Height, Is.EqualTo(height));
        });
    }

    [Test]
    public void Can_Create_HeadlineBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 200;
        int? height = 100;
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;

        HeadlineBlock headlineBlock = new(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height);

        Assert.Multiple(() =>
        {
            Assert.That(headlineBlock.Id, Is.EqualTo(id));
            Assert.That(headlineBlock.IsOptional, Is.EqualTo(isOptional));
            Assert.That(headlineBlock.XPosition, Is.EqualTo(xPosition));
            Assert.That(headlineBlock.YPosition, Is.EqualTo(yPosition));
            Assert.That(headlineBlock.Width, Is.EqualTo(width));
            Assert.That(headlineBlock.Height, Is.EqualTo(height));
            Assert.That(headlineBlock.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(headlineBlock.DateModified, Is.EqualTo(dateModified));
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

        Assert.That(() => new HeadlineBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = -1;
        const int width = 200;
        int? height = 100;

        Assert.That(() => new HeadlineBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        const bool isOptional = true;
        const int xPosition = 10;
        const int yPosition = 15;
        const int width = 0;
        int? height = 100;

        Assert.That(() => new HeadlineBlock(isOptional, xPosition, yPosition, width, height), Throws.ArgumentException);
    }
}
