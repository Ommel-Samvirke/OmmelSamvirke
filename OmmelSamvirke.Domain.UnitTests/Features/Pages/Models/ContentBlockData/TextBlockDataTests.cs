using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlockData;

[TestFixture]
public class TextBlockDataTests : PagesBaseTestModel
{

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
    }

    [Test]
    public void Can_Create_TextBlockData_With_Valid_Data()
    {
        const int pageId = 1;
        const string text = "Some valid text";
        TextBlockData textBlockData = new(DefaultTextBlock, text, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(textBlockData.ContentBlock, Is.EqualTo(DefaultTextBlock));
            Assert.That(textBlockData.Text, Is.EqualTo(text));
            Assert.That(textBlockData.PageId, Is.EqualTo(pageId));
        });
    }

    [Test]
    public void Can_Create_TextBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const int pageId = 1;
        const string text = "Some valid text";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        TextBlockData textBlockData = new(id, dateCreated, dateModified, DefaultTextBlock, text, pageId);

        Assert.Multiple(() =>
        {
            Assert.That(textBlockData.Id, Is.EqualTo(id));
            Assert.That(textBlockData.ContentBlock, Is.EqualTo(DefaultTextBlock));
            Assert.That(textBlockData.Text, Is.EqualTo(text));
            Assert.That(textBlockData.PageId, Is.EqualTo(pageId));
            Assert.That(textBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(textBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const int pageId = 1;
        const string text = "Some valid text";
        TextBlock nullContentBlock = null!;

        Assert.That(() => new TextBlockData(nullContentBlock, text, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Text_Is_Empty()
    {
        const int pageId = 1;
        const string text = "";

        Assert.That(() => new TextBlockData(DefaultTextBlock, text, pageId), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Text_Is_Too_Long()
    {
        const int pageId = 1;
        string text = new string('a', 5001);

        Assert.That(() => new TextBlockData(DefaultTextBlock, text, pageId), Throws.ArgumentException);
    }
}
