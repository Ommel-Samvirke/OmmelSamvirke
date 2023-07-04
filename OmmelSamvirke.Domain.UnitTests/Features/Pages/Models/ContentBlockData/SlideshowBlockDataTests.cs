using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class SlideshowBlockDataTests : PagesBaseTestModel
{

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_SlideshowBlockData_With_Valid_Data()
    {
        const int pageId = 1;
        List<string> imageUrls = new() { "https://example.com/image1.jpg", "https://example.com/image2.jpg" };
        SlideshowBlockData slideshowBlockData = new(DefaultSlideshowBlock, imageUrls, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlockData.ContentBlock, Is.EqualTo(DefaultSlideshowBlock));
            CollectionAssert.AreEqual(slideshowBlockData.ImageUrls, imageUrls);
            Assert.That(slideshowBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_SlideshowBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        List<string> imageUrls = new() { "https://example.com/image1.jpg", "https://example.com/image2.jpg" };
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        SlideshowBlockData slideshowBlockData = new(id, dateCreated, dateModified, DefaultSlideshowBlock, imageUrls, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlockData.Id, Is.EqualTo(id));
            Assert.That(slideshowBlockData.ContentBlock, Is.EqualTo(DefaultSlideshowBlock));
            CollectionAssert.AreEqual(slideshowBlockData.ImageUrls, imageUrls);
            Assert.That(slideshowBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(slideshowBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(slideshowBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        List<string> imageUrls = new() { "https://example.com/image1.jpg", "https://example.com/image2.jpg" };
        SlideshowBlock nullContentBlock = null!;

        Assert.That(() => new SlideshowBlockData(nullContentBlock, imageUrls, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Is_Null()
    {
        const int pageId = 1;
        List<string> nullImageUrls = null!;

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, nullImageUrls, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Contains_Empty_Url()
    {
        const int pageId = 1;
        List<string> imageUrls = new() { "https://example.com/image1.jpg", "" };

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, imageUrls, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Contains_Url_Too_Long()
    {
        const int pageId = 1;
        string longUrl = new('a', 2001);
        List<string> imageUrls = new() { "https://example.com/image1.jpg", longUrl };

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, imageUrls, pageId), Throws.ArgumentException);
    }
}
