using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

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
        const int pageId = 1;
        const string imageUrl = "https://example.com/someimage.jpg";
        ImageBlockData imageBlockData = new(DefaultImageBlock, imageUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlockData.ContentBlock, Is.EqualTo(DefaultImageBlock));
            Assert.That(imageBlockData.ImageUrl, Is.EqualTo(imageUrl));
            Assert.That(imageBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_ImageBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        const string imageUrl = "https://example.com/someimage.jpg";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        ImageBlockData imageBlockData = new(id, dateCreated, dateModified, DefaultImageBlock, imageUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(imageBlockData.Id, Is.EqualTo(id));
            Assert.That(imageBlockData.ContentBlock, Is.EqualTo(DefaultImageBlock));
            Assert.That(imageBlockData.ImageUrl, Is.EqualTo(imageUrl));
            Assert.That(imageBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(imageBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(imageBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        const string imageUrl = "https://example.com/someimage.jpg";
        ImageBlock nullContentBlock = null!;

        Assert.That(() => new ImageBlockData(nullContentBlock, imageUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrl_Is_Empty()
    {
        const int pageId = 1;
        const string imageUrl = "";

        Assert.That(() => new ImageBlockData(DefaultImageBlock, imageUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrl_Is_Too_Long()
    {
        const int pageId = 1;
        string imageUrl = new string('a', 2001);

        Assert.That(() => new ImageBlockData(DefaultImageBlock, imageUrl, pageId), Throws.ArgumentException);
    }
}
