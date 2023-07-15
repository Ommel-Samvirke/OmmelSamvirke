using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class ImageBlockDataTests : PagesBaseTestModel
{
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_ImageBlockData_With_Valid_Data()
    {
        Url imageUrl = new("https://example.com/someimage.jpg");
        ImageBlockData imageBlockData = new(DefaultImageBlock, imageUrl, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlockData.ContentBlock, Is.EqualTo(DefaultImageBlock));
            Assert.That(imageBlockData.ImageUrl, Is.EqualTo(imageUrl));
            Assert.That(imageBlockData.Page, Is.EqualTo(DefaultPage));
        });
    }

    [Test]
    public void Can_Create_ImageBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        Url imageUrl = new("https://example.com/someimage.jpg");
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        ImageBlockData imageBlockData = new(id, dateCreated, dateModified, DefaultImageBlock, imageUrl, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlockData.Id, Is.EqualTo(id));
            Assert.That(imageBlockData.ContentBlock, Is.EqualTo(DefaultImageBlock));
            Assert.That(imageBlockData.ImageUrl, Is.EqualTo(imageUrl));
            Assert.That(imageBlockData.Page, Is.EqualTo(DefaultPage));
            Assert.That(imageBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(imageBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        Url imageUrl = new("https://example.com/someimage.jpg");
        ImageBlock nullContentBlock = null!;

        Assert.That(() => new ImageBlockData(nullContentBlock, imageUrl, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrl_Is_Empty()
    {
        Url imageUrl = new("");

        Assert.That(() => new ImageBlockData(DefaultImageBlock, imageUrl, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrl_Is_Too_Long()
    {
        Url imageUrl = new(new string('a', 2001));

        Assert.That(() => new ImageBlockData(DefaultImageBlock, imageUrl, DefaultPage), Throws.ArgumentException);
    }
}
