using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

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
        const int pageId = 1;
        const string videoUrl = "https://example.com/somevideo.mp4";
        VideoBlockData videoBlockData = new(DefaultVideoBlock, videoUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlockData.ContentBlock, Is.EqualTo(DefaultVideoBlock));
            Assert.That(videoBlockData.VideoUrl, Is.EqualTo(videoUrl));
            Assert.That(videoBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_VideoBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        const string videoUrl = "https://example.com/somevideo.mp4";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        VideoBlockData videoBlockData = new(id, dateCreated, dateModified, DefaultVideoBlock, videoUrl, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(videoBlockData.Id, Is.EqualTo(id));
            Assert.That(videoBlockData.ContentBlock, Is.EqualTo(DefaultVideoBlock));
            Assert.That(videoBlockData.VideoUrl, Is.EqualTo(videoUrl));
            Assert.That(videoBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(videoBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(videoBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        const string videoUrl = "https://example.com/somevideo.mp4";
        VideoBlock nullContentBlock = null!;

        Assert.That(() => new VideoBlockData(nullContentBlock, videoUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_VideoUrl_Is_Empty()
    {
        const int pageId = 1;
        const string videoUrl = "";

        Assert.That(() => new VideoBlockData(DefaultVideoBlock, videoUrl, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_VideoUrl_Is_Too_Long()
    {
        const int pageId = 1;
        string videoUrl = new('a', 2001);

        Assert.That(() => new VideoBlockData(DefaultVideoBlock, videoUrl, pageId), Throws.ArgumentException);
    }
}
