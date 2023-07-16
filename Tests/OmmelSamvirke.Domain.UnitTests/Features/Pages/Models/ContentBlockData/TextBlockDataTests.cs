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
        const string text = "Some valid text";
        TextBlockData textBlockData = new(DefaultTextBlock, text, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(textBlockData.ContentBlock, Is.EqualTo(DefaultTextBlock));
            Assert.That(textBlockData.Text, Is.EqualTo(text));
            Assert.That(textBlockData.Page, Is.EqualTo(DefaultPage));
        });
    }

    [Test]
    public void Can_Create_TextBlockData_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const string text = "Some valid text";
        DateTime dateCreated = DateTime.UtcNow;
        DateTime dateModified = DateTime.UtcNow;
        TextBlockData textBlockData = new(id, dateCreated, dateModified, DefaultTextBlock, text, DefaultPage);

        Assert.Multiple(() =>
        {
            Assert.That(textBlockData.Id, Is.EqualTo(id));
            Assert.That(textBlockData.ContentBlock, Is.EqualTo(DefaultTextBlock));
            Assert.That(textBlockData.Text, Is.EqualTo(text));
            Assert.That(textBlockData.Page, Is.EqualTo(DefaultPage));
            Assert.That(textBlockData.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(textBlockData.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_ContentBlock_Is_Null()
    {
        const string text = "Some valid text";
        TextBlock nullContentBlock = null!;

        Assert.That(() => new TextBlockData(nullContentBlock, text, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Text_Is_Empty()
    {
        const string text = "";

        Assert.That(() => new TextBlockData(DefaultTextBlock, text, DefaultPage), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Text_Is_Too_Long()
    {
        string text = new('a', 5001);

        Assert.That(() => new TextBlockData(DefaultTextBlock, text, DefaultPage), Throws.ArgumentException);
    }
}
