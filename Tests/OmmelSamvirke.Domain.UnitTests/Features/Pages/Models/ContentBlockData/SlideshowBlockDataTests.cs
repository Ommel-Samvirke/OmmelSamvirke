using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

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
        List<Url> imageUrls = new()
        {
            new Url("https://example.com/image1.jpg"),
            new Url("https://example.com/image2.jpg")

        };
        SlideshowBlockData slideshowBlockData = new(DefaultSlideshowBlock, imageUrls, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlockData.ContentBlock, Is.EqualTo(DefaultSlideshowBlock));
            CollectionAssert.AreEqual(slideshowBlockData.ImageUrls, imageUrls);
            Assert.That(slideshowBlockData.Page, Is.EqualTo(DefaultPage));
        });
    }

    [Test]
    public void Can_Create_SlideshowBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        List<Url> imageUrls = new()
        {
            new Url("https://example.com/image1.jpg"),
            new Url("https://example.com/image2.jpg")

        };
        DateTime dateCreated = DateTime.UtcNow;
        DateTime dateModified = DateTime.UtcNow;
        SlideshowBlockData slideshowBlockData = new(id, dateCreated, dateModified, DefaultSlideshowBlock, imageUrls, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(slideshowBlockData.Id, Is.EqualTo(id));
            Assert.That(slideshowBlockData.ContentBlock, Is.EqualTo(DefaultSlideshowBlock));
            CollectionAssert.AreEqual(slideshowBlockData.ImageUrls, imageUrls);
            Assert.That(slideshowBlockData.Page, Is.EqualTo(DefaultPage));
            Assert.That(slideshowBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(slideshowBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        List<Url> imageUrls = new()
        {
            new Url("https://example.com/image1.jpg"),
            new Url("https://example.com/image2.jpg")

        };
        SlideshowBlock nullContentBlock = null!;

        Assert.That(() => new SlideshowBlockData(nullContentBlock, imageUrls, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Is_Null()
    {
        List<Url> nullImageUrls = null!;

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, nullImageUrls, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Contains_Empty_Url()
    {
        List<Url> imageUrls = new()
        {
            new Url("https://example.com/image1.jpg"),
            new Url("")

        };

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, imageUrls, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrls_Contains_Url_Too_Long()
    {
        string longUrl = new('a', 2001);
        List<Url> imageUrls = new()
        {
            new Url("https://example.com/image1.jpg"),
            new Url(longUrl)

        };

        Assert.That(() => new SlideshowBlockData(DefaultSlideshowBlock, imageUrls, DefaultPage), Throws.ArgumentException);
    }
}
