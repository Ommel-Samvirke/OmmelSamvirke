using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class VideoBlockDataTests : PagesBaseTestModel
{

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_VideoBlockData_With_Valid_Data()
    {
        Url videoUrl = new("https://example.com/somevideo.mp4");
        VideoBlockData videoBlockData = new(DefaultVideoBlock, videoUrl, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlockData.ContentBlock, Is.EqualTo(DefaultVideoBlock));
            Assert.That(videoBlockData.VideoUrl, Is.EqualTo(videoUrl));
            Assert.That(videoBlockData.Page, Is.EqualTo(DefaultPage));
        });
    }

    [Test]
    public void Can_Create_VideoBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        Url videoUrl = new("https://example.com/somevideo.mp4");
        DateTime dateCreated = DateTime.UtcNow;
        DateTime dateModified = DateTime.UtcNow;
        VideoBlockData videoBlockData = new(id, dateCreated, dateModified, DefaultVideoBlock, videoUrl, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlockData.Id, Is.EqualTo(id));
            Assert.That(videoBlockData.ContentBlock, Is.EqualTo(DefaultVideoBlock));
            Assert.That(videoBlockData.VideoUrl, Is.EqualTo(videoUrl));
            Assert.That(videoBlockData.Page, Is.EqualTo(DefaultPage));
            Assert.That(videoBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(videoBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        Url videoUrl = new("https://example.com/somevideo.mp4");
        VideoBlock nullContentBlock = null!;

        Assert.That(() => new VideoBlockData(nullContentBlock, videoUrl, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_VideoUrl_Is_Empty()
    {
        Url videoUrl = new("");

        Assert.That(() => new VideoBlockData(DefaultVideoBlock, videoUrl, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_VideoUrl_Is_Too_Long()
    {
        Url videoUrl = new(new string('a', 2001));

        Assert.That(() => new VideoBlockData(DefaultVideoBlock, videoUrl, DefaultPage), Throws.ArgumentException);
    }
}
