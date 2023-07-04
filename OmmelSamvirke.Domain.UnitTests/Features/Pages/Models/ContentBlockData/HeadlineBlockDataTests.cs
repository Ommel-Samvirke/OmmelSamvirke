using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class HeadlineBlockDataTests : PagesBaseTestModel
{
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_HeadlineBlockData_With_Valid_Data()
    {
        const int pageId = 1;
        const string headline = "Some valid headline";
        HeadlineBlockData headlineBlockData = new(DefaultHeadlineBlock, headline, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(headlineBlockData.ContentBlock, Is.EqualTo(DefaultHeadlineBlock));
            Assert.That(headlineBlockData.Headline, Is.EqualTo(headline));
            Assert.That(headlineBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_HeadlineBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        const string headline = "Some valid headline";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        HeadlineBlockData headlineBlockData = new(id, dateCreated, dateModified, DefaultHeadlineBlock, headline, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(headlineBlockData.Id, Is.EqualTo(id));
            Assert.That(headlineBlockData.ContentBlock, Is.EqualTo(DefaultHeadlineBlock));
            Assert.That(headlineBlockData.Headline, Is.EqualTo(headline));
            Assert.That(headlineBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(headlineBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(headlineBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        const string headline = "Some valid headline";
        HeadlineBlock nullContentBlock = null!;

        Assert.That(() => new HeadlineBlockData(nullContentBlock, headline, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Headline_Is_Empty()
    {
        const int pageId = 1;
        const string headline = "";

        Assert.That(() => new HeadlineBlockData(DefaultHeadlineBlock, headline, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Headline_Is_Too_Long()
    {
        const int pageId = 1;
        string headline = new string('a', 201);

        Assert.That(() => new HeadlineBlockData(DefaultHeadlineBlock, headline, pageId), Throws.ArgumentException);
    }
}
